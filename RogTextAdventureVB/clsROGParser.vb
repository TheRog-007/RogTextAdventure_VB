Imports System.Net.Http.Headers

Public Class clsROGParser
    'Created 23/07/2024 By Roger Williams
    '
    'What It Does
    '------------
    '
    'checks passed text to try and determine if statement valid
    '
    '- passes through all the lists
    '- checks for duplicates
    '- checks valid querry
    '- uses primitive grammar rules
    '
    'e.g.:
    '
    'open door with the key
    '
    'run away
    '
    'would produce response:
    ' which direction?
    '
    'run away north
    '
    'would produce response:
    '  you ran north
    '
    '
    'NOTE: this phase one version is only passed direction commands
    '

    Public isOk As Boolean = True    'used externally to determine if statement valid
    'available to caller to see key elements of what user typed
    Public Noun As String = ""
    Public Verb As String = ""
    Public Adjective As String = ""
    Public Preposition As String = ""
    Public Direction As String = ""

    'internal lists
    Private ReadOnly lstVerbs As New List(Of String)({"be", "have", "do", "go", "get", "make", "know", "take", "see", "look", "give", "need", "put", "get", "let", "begin", "create", "start", "run", "move", "creep",
                                                    "hold", "use", "include", "set", "stop", "allow", "appear", "destroy", "kill", "disable", "enable", "open", "close", "run", "talk", "listen", "walk"})
    Private ReadOnly lstNouns As New List(Of String)({"exit", "my", "you", "them", "they", "him", "she", "me", "their", "knife", "apple", "bread", "sword", "dragon", "knight", "key", "plate", "candle", "matches", "door", "exit"})
    Private ReadOnly lstAdjectives As New List(Of String)({"new", "old", "box", "first", "last", "current", "low", "high", "partial", "full", "common", "late", "early", "on", "used", "alert", "away", "forward", "backward",
                                                  "left", "right"})
    Private ReadOnly lstPrepositions As New List(Of String)({"in", "of", "with", "to", "behind", "when", "why", "while", "kind", "by", "under", "before", "up", "down", "between"})
    Private ReadOnly lstDirections As New List(Of String)({"north", "south", "east", "west"})

    'used when user types HELP LIST <verbs><nouns><adjectives><Prepositions><directions>
    Private ReadOnly lstHelpWords As New List(Of String)({"Verbs", "Nouns", "Adjectives", "Prepositions", "Directions"})
    'NOTE: the enumeration value is in the SAME order as the lists as created
    Private Enum enumWordTypes
        Verbs = 0
        Nouns = 1
        Adjectives = 2
        Prepositions = 3
        Directions = 4
    End Enum


    Public Sub Help_ListValidWords(bytWhat As Byte)
        'Created 23/07/2024 By Roger Williams
        '
        'when users types: HELP LIST VERBS
        '
        'runs this sub which shows them on the console
        '
        'VARS
        '
        'bytWhat    : what to show (uses enum) 0=verb 1=noun etc.
        '
        '
        '
        Dim strOutput As String = ""
        Dim strTemp As String
        Dim intNum As Integer = 1
        'make sure help is only text on screen
        '
        'NOTE: for later phases how about a SECOND console with the help soley on it?
        '      tis the age of 20 inch monitors after all..
        Console.Clear()
        'which list is required?
        Select Case bytWhat
            Case 0 'verbs
                Console.WriteLine("Valid Verbs")
                'print list contents to console
                For Each strTemp In lstVerbs
                    'puposely append to string
                    strOutput = strOutput & strTemp & " "
                    intNum += 1
                    'print string when 10 commands in it to stop unwanted word wrap
                    If intNum = 10 Then
                        Console.WriteLine(strOutput)
                        'reset vars
                        strOutput = ""
                        intNum = 1
                    End If
                Next
'NOTE: above procews is repeated for each of the list types
            Case 1  'nouns
                Console.WriteLine("Valid Nouns")

                For Each strTemp In lstNouns
                    strOutput = strOutput & strTemp & " "
                    intNum += 1

                    If intNum = 10 Then
                        Console.WriteLine(strOutput)
                        strOutput = ""
                        intNum = 1
                    End If
                Next
            Case 2 'adjectives
                Console.WriteLine("Valid Adjectives")

                For Each strTemp In lstVerbs
                    strOutput = strOutput & strTemp & " "
                    intNum += 1

                    If intNum = 10 Then
                        Console.WriteLine(strOutput)
                        strOutput = ""
                        intNum = 1
                    End If
                Next
            Case 3 'prepositions
                Console.WriteLine("Valid Prepositions")

                For Each strTemp In lstPrepositions
                    strOutput = strOutput & strTemp & " "
                    intNum += 1

                    If intNum = 10 Then
                        Console.WriteLine(strOutput)
                        strOutput = ""
                        intNum = 1
                    End If
                Next
            Case 4 'directions
                Console.WriteLine("Valid Directions")

                For Each strTemp In lstDirections
                    strOutput = strOutput & strTemp & " "
                    intNum += 1

                    If intNum = 10 Then
                        Console.WriteLine(strOutput)
                        strOutput = ""
                        intNum = 1
                    End If
                Next
        End Select

        'check if string not null if so write to console
        'NOTE: is there a better way to do this?
        If strOutput.Length <> 0 Then
            Console.WriteLine(strOutput)
        End If
        'write blank line
        Console.WriteLine()
    End Sub
    Private Sub Help_List()
        'Created 24/07/2024 By Roger Williams
        '
        'Lists the available help options when user types: HELP
        '
        'NOTE: for later phases could all these options be shown in SECOND console?
        '
        Console.Clear()
        Console.WriteLine("Help Options")
        Console.WriteLine("============")
        Console.WriteLine("")
        Console.WriteLine("List available adjectives             - help list adjectives")
        Console.WriteLine("List available verbs                  - help list verbs")
        Console.WriteLine("List available nouns                  - help list nouns")
        Console.WriteLine("List available prepositions           - help list prepositions")
        Console.WriteLine("List available directions of movement - help list directions")
        Console.WriteLine("")
        Console.WriteLine("Enter: exit - at any time to end the game")
        Console.WriteLine("")
    End Sub
    Private Function ContainsValidWords(strWhat As String, bytWhat As Byte) As Boolean
        'Created 23/07/2024 By Roger Williams
        '
        'checks if strPhrase contains verb,noun,adjective,preposition,direction
        '
        'VARS
        '
        'strWhat    : what to search
        'bytWhat    : what to check for (enum) verb,noun etc
        '
        'returns true if finds valid phrase/word
        'also populates public class vars:
        '
        'noun
        'verb
        'adjective
        'preposition
        'direction
        '
        Dim strTemp As String
        Dim blnOK As Boolean = False
        'what to check
        Select Case bytWhat
            Case 0 'verbs
                'iterate through the list looking for the required value
                For Each strTemp In lstVerbs
                    'does any part of the passed string exist in the list? 
                    If strWhat.IndexOf(strTemp) <> -1 Then
                        'set public variable
                        Verb = strTemp
                        'say ok then exit loop - saving processor cycles!
                        blnOK = True
                        Exit Select
                    End If
                Next
'NOTE: above process copied for rest of the options
            Case 1  'nouns
                For Each strTemp In lstNouns
                    If strWhat.IndexOf(strTemp) <> -1 Then
                        Noun = strTemp
                        blnOK = True
                        Exit Select
                    End If
                Next
            Case 2 'adjectives
                For Each strTemp In lstAdjectives
                    If strWhat.IndexOf(strTemp) <> -1 Then
                        Adjective = strTemp
                        blnOK = True
                        Exit Select
                    End If
                Next
            Case 3 'prepositions
                For Each strTemp In lstPrepositions
                    If strWhat.IndexOf(strTemp) <> -1 Then
                        Preposition = strTemp
                        blnOK = True
                        Exit Select
                    End If
                Next
            Case 4 'directions
                For Each strTemp In lstDirections
                    If strWhat.IndexOf(strTemp) <> -1 Then
                        Direction = strTemp
                        blnOK = True
                        Exit Select
                    End If
                Next
                'no need for a case..else as fixed values are sent
        End Select

        Return blnOK
    End Function

    Public Sub ParseText(strWhat As String)
        'Created 23/07/2024 By Roger Williams
        '
        'checks if text contains valid words e.g. nouns sets IsOk accordingly
        '
        'Rules
        '-----
        '
        'every phrase should contain a verb
        'every verb should either have an adjective e.g. open door
        'or
        'a preposition e.g. while
        'or
        'a noun e.g. key
        '
        'also handles user help requests, valid request string are:
        '
        'HELP
        '
        'HELP LIST <what>
        '
        '<what> types:
        '
        '          VERBS
        '          NOUNS
        '          ADJECTIVES
        '          PREPOSITIONS
        '          DIRECTIONS
        '
        '
        Dim bytValid As Byte = 0
        Dim strTemp As String = ""
        'set check variables too false
        Dim blnAdjective As Boolean = False
        Dim blnDirection As Boolean = False
        Dim blnNoun As Boolean = False
        Dim blnPreposition As Boolean = False
        Dim blnVerb As Boolean = False

        'if passed string has no value leave and set error to true
        If strWhat.Length = 0 Then
            isOk = False
        Else
            'clear public vars
            Noun = ""
            Adjective = ""
            Verb = ""
            Preposition = ""
            Direction = ""

            'convert to lowercase
            strWhat = strWhat.ToLower
            'check if help request
            If strWhat.IndexOf("help") <> -1 Then
                If strWhat = "help" Then
                    Help_List()
                End If
                'check if user asking for a list
                If strWhat.IndexOf("help list ") <> -1 Then
                    strTemp = strWhat.Substring(InStrRev(strWhat, " "), strWhat.Length - InStrRev(strWhat, " "))

                    Select Case strTemp
                        Case "verbs"
                            Help_ListValidWords(enumWordTypes.Verbs)
                        Case "adjectives"
                            Help_ListValidWords(enumWordTypes.Adjectives)
                        Case "nouns"
                            Help_ListValidWords(enumWordTypes.Nouns)
                        Case "prepositions"
                            Help_ListValidWords(enumWordTypes.Prepositions)
                        Case "directions"
                            Help_ListValidWords(enumWordTypes.Directions)
                    End Select
                End If
            Else
                'every phrase should contain a verb
                'every verb should either have an
                '
                'adjective e.g. open door
                'or
                'a preposition e.g. while
                'or
                'a noun e.g. key
                '
                If ContainsValidWords(strWhat, enumWordTypes.Adjectives) Then blnAdjective = True
                If ContainsValidWords(strWhat, enumWordTypes.Directions) Then blnDirection = True
                If ContainsValidWords(strWhat, enumWordTypes.Nouns) Then blnNoun = True
                If ContainsValidWords(strWhat, enumWordTypes.Prepositions) Then blnPreposition = True
                If ContainsValidWords(strWhat, enumWordTypes.Verbs) Then blnVerb = True

                'mow look at the rules
                'NOTE: these are primitive grammar rules and need to be expanded and developed
                If blnVerb Then bytValid = 1
                If blnAdjective And blnVerb Then bytValid += 1
                If blnPreposition And blnVerb Then bytValid += 1
                If blnNoun And blnVerb Then bytValid += 1

                'if valid phrase ot user typed "exit"
                If (bytValid > 0) Or (Noun = "exit") Then
                    isOk = True
                Else
                    'if not containing any valid words set to incorrect phrase
                    Console.WriteLine(strWhat & " - Not Recognised Phrase")
                    isOk = False
                End If
            End If
        End If
    End Sub


End Class

