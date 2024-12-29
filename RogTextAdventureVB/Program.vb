Imports System
Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading
'Created 23/07/2024 By Roger Williams
'
'text adventure game!
'
'features a basic parser!
'
'This is Phase one of a potential three phase developmet:
'
'Phase One   - basic game design with movement and primitive parser framework
'Phase Two   - add objects into the rooms and ability to interact better parser
'Phase Three - add entities and action rooms where a list of actions is available e.g. run, stop etc.
'
'NOTE: currently levels have 20 lines of descriptive text, allowing for the 42 row screen leaves lines for future use
'
'
Module Program
    Dim clsParser As New clsROGParser                  'the parser class
    Dim lstRooms As New List(Of clsGameRooms)          'collection of all rooms in the level
    Dim clsCurRoom As clsGameRooms                     'current rrom player is in   
    Dim intCurRoom As Integer = 0                      'ID of above

    Sub ShowRoom(intWhat As Integer)
        'Created 24/07/2024 By Roger Williams
        '
        'writes room description fields to console
        '

        'clear console and set cursor in the "home" position
        Console.CursorTop = 0
        Console.Clear()
        'set console for main game look
        Console.BackgroundColor = ConsoleColor.DarkBlue
        Console.ForegroundColor = ConsoleColor.Yellow

        'only use intWhat if first room
        If intWhat = 1 Then
            intCurRoom = intWhat
            clsCurRoom = lstRooms(intWhat)
        End If

        'write room description
        Console.WriteLine(clsCurRoom.Desc1)
        Console.WriteLine(clsCurRoom.Desc2)
        Console.WriteLine(clsCurRoom.Desc3)
        Console.WriteLine(clsCurRoom.Desc4)
        Console.WriteLine(clsCurRoom.Desc5)
        Console.WriteLine(clsCurRoom.Desc6)
        Console.WriteLine(clsCurRoom.Desc7)
        Console.WriteLine(clsCurRoom.Desc8)
        Console.WriteLine(clsCurRoom.Desc9)
        Console.WriteLine(clsCurRoom.Desc10)
        Console.WriteLine(clsCurRoom.Desc11)
        Console.WriteLine(clsCurRoom.Desc12)
        Console.WriteLine(clsCurRoom.Desc13)
        Console.WriteLine(clsCurRoom.Desc14)
        Console.WriteLine(clsCurRoom.Desc15)
        Console.WriteLine(clsCurRoom.Desc16)
        Console.WriteLine(clsCurRoom.Desc17)
        Console.WriteLine(clsCurRoom.Desc18)
        Console.WriteLine(clsCurRoom.Desc19)
        Console.WriteLine(clsCurRoom.Desc20)
        Console.WriteLine()
        Console.WriteLine("Enter Command:")
    End Sub

    Sub ShowTitle()
        'Created 23/07/2024 By Roger Williams
        '
        'shows title screen
        '
        'works thus:
        '
        'the intro screen comprises of TWO text files:
        '
        '    introscr1.txt
        '    introscr2.txt
        '
        'these are loaded into two variables then written one at a time to the console and the
        'background/foreground colour is changed
        '
        '
        Dim bytNum As Byte = 0
        Dim strmIntro1 As StreamReader  'used for readaing the text files in
        Dim strmIntro2 As StreamReader
        Dim strIntro1 As String = ""    'used for storing intro level data
        Dim strIntro2 As String = ""

        'read intro screen files into strings
        strmIntro1 = New StreamReader("INTROSCR1.txt")
        strIntro1 = strmIntro1.ReadToEnd()
        strmIntro2 = New StreamReader("INTROSCR2.txt")
        strIntro2 = strmIntro2.ReadToEnd()
        'set console colour
        Console.ForegroundColor = ConsoleColor.White

        'iterate changing console colour 6 times
        For bytNum = 0 To 6
            Console.Clear()

            'if 0 show one colour else show other
            If bytNum Mod 2 = 0 Then
                Console.BackgroundColor = ConsoleColor.Cyan
                Console.WriteLine(strIntro1)
            Else
                Console.BackgroundColor = ConsoleColor.Blue
                Console.WriteLine(strIntro1)

            End If
            'wait to give user chance to see the colour change
            Thread.Sleep(1000)
        Next

        Console.Clear()
        'iterate changing console colour 6 times
        For bytNum = 0 To 6
            Console.Clear()

            If bytNum Mod 2 = 0 Then
                Console.BackgroundColor = ConsoleColor.DarkBlue
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(strIntro2)
            Else
                Console.BackgroundColor = ConsoleColor.Blue
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(strIntro2)
            End If
            'wait to give user chance to see the colour change
            Thread.Sleep(1000)
        Next

        strmIntro1.Dispose()
        strmIntro2.Dispose()
        'show first proper game screen
        'display room 1
        ShowRoom(1)
    End Sub

    Sub LoadLeve1()
        'Created 23/07/2024 By Roger Williams
        '
        'loads level 1 from level1.txt into lstRooms which is a collection of clsGameRooms
        'level text file format matches the class structure
        '
        Dim strmRead As StreamReader
        Dim strTemp As String = ""

        strmRead = New StreamReader("level1.txt")
        'clear incase already contains data
        lstRooms.Clear()
        'first add blank class object as we need 0 to represent null and index starts at 1
        clsCurRoom = New clsGameRooms()
        lstRooms.Add(clsCurRoom)

        'read level data
        While Not strmRead.EndOfStream
            'recreate the class object else carries over previous values!
            clsCurRoom = New clsGameRooms()
            'read level lines and store in class
            strTemp = strmRead.ReadLine()
            clsCurRoom.ID = CInt(strTemp)
            strTemp = strmRead.ReadLine()
            clsCurRoom.NextRoomNorth = CInt(strTemp)
            strTemp = strmRead.ReadLine()
            clsCurRoom.NextRoomSouth = CInt(strTemp)
            strTemp = strmRead.ReadLine()
            clsCurRoom.NextRoomEast = CInt(strTemp)
            strTemp = strmRead.ReadLine()
            clsCurRoom.NextRoomWest = CInt(strTemp)
            'read rooms description
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc1 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc2 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc3 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc4 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc5 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc6 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc7 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc8 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc9 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc10 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc11 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc12 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc13 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc14 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc15 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc16 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc17 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc18 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc19 = strTemp
            strTemp = strmRead.ReadLine()
            clsCurRoom.Desc20 = strTemp
            'store in level room list
            lstRooms.Add(clsCurRoom)
        End While
        'close level text file
        strmRead.Close()
        strmRead.Dispose()
    End Sub
    Sub ExitProgram()
        'Created 24/07/2024 By Roger Williams
        '
        'technically pointless as the console will just close, but in Visual Stdudio it will wait for a keypress!
        '
        Console.WriteLine("Bye!")
    End Sub
    Sub Init()
        'Created 23/07/2024 By Roger Williams
        '
        'initialises the console, set title, colours etc and show title screen, loads level
        '
        '
        LoadLeve1()
        Console.Title = "Rog's Adventure!"
        Console.WindowHeight = 42 'NOTE height is measure in ROWS
        Console.WindowWidth = 121 'NOTE height is measure in COLUMNS
        Console.Clear()
        'show intro
        ShowTitle()
    End Sub

    Function CheckIfEnd() As Boolean
        'Created 30/07/2024 By Roger Williams
        '
        'checks if there are NO directions the player can move to
        '
        'this means end of game!
        '
        '
        Return clsCurRoom.NextRoomEast = 0 And clsCurRoom.NextRoomNorth = 0 And clsCurRoom.NextRoomSouth = 0 And clsCurRoom.NextRoomWest = 0
    End Function
    Sub Main(args As String())
        'Created 23/07/2024 By Roger Williams
        '
        'main routine for the game
        '
        '
        Dim strInput As String = ""
        Dim intNum As Integer = 0
        'configure the console and show intro
        Init()

        'loop till user wants to leave
        While strInput <> "exit"
            'get user instruction
            strInput = Console.ReadLine()
            'validate entry
            clsParser.ParseText(strInput)

            'if it ok?
            If clsParser.isOk Then
                'check for movement verb
                If strInput.IndexOf("go ") <> -1 Or strInput.IndexOf("move ") <> -1 Then
                    'set to current room number - why? because if the direction is VALID
                    'the room number will change
                    intNum = intCurRoom

                    'south is forward, north backward, east/west left/right
                    Select Case clsParser.Direction
                        Case "north"
                            If clsCurRoom.NextRoomNorth <> 0 Then
                                'move north
                                intCurRoom = clsCurRoom.NextRoomNorth
                            End If
                        Case "south"
                            If clsCurRoom.NextRoomSouth <> 0 Then
                                'move south
                                intCurRoom = clsCurRoom.NextRoomSouth
                            End If
                        Case "east"
                            If clsCurRoom.NextRoomEast <> 0 Then
                                'move east
                                intCurRoom = clsCurRoom.NextRoomEast
                            End If
                        Case "west"
                            If clsCurRoom.NextRoomWest <> 0 Then
                                'move west
                                intCurRoom = clsCurRoom.NextRoomWest
                            End If
                        Case Else
                            'direction entered does not exist?
                            Console.WriteLine("Sorry! - Direction entered isnt available! Please try again")
                            'wait before redrawing screen
                            Thread.Sleep(3000)
                    End Select

                    'if command not acceptable dont change rooms
                    If intNum = intCurRoom Then
                        Console.WriteLine("Sorry! - Direction entered isnt available! Please try again")
                        strInput = ""
						'wait before redrawing screen
                        Thread.Sleep(3000)
                    End If

                    'shows new or even existing room
                    clsCurRoom = lstRooms.Find(Function(clsCurRoomsFind) clsCurRoomsFind.ID = intCurRoom)
                    'store the room ID - for future development
                    intCurRoom = clsCurRoom.ID
                    'show room to player pass 0 as not first room
                    ShowRoom(0)

                    'has user lost/won the game?
                    If CheckIfEnd() Then
                        'set text input to "exit" this causes the game to end
                        strInput = "exit"
                    End If
                Else
                    'ignore help and exit commands only show error for commands not understood
                    'NOTE: check game logic - can this be refactored away?
                    If Not strInput.Contains("help") And strInput <> "exit" Then
                        Console.WriteLine("Unregonised command, please try again!")
                    End If

                    'ignore exit command
                    If strInput <> "exit" Then
                        'clear last command
                        strInput = ""
                        'give user time to see error
                        Thread.Sleep(4000)
                        ShowRoom(0)
                    End If
                End If
            Else
                'if command not understood and not "exit"
                If strInput <> "exit" Then
                    Console.WriteLine("Unregonised command, please try again!")
                    'clear last command
                    strInput = ""
                    'give user time to see error
                    Thread.Sleep(2000)
                    ShowRoom(0)
                End If
            End If
        End While
        'Bye!
        ExitProgram()
    End Sub
End Module
