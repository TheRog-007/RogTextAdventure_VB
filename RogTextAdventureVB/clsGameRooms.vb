'Created 24/07/2024 By Roger Williams
'
'used to store rooms for each level in memory
'
'uses a class structure and main program uses list of this class to represent the entire level
'room data stored in a text file which the main program reads and stores each room data into
'its own class object - not very 80s!
'
'
'
Public Class clsGameRooms
    Public ID As Integer = 0
    'disabled 24/07/2024 not needed as new nextroom property does a better job!
    'Public NorthExit As Boolean = False
    'Public SouthExit As Boolean = False
    'Public EastExit As Boolean = False
    'Public WestExit As Boolean = False

    'next 4 propoerties determine which room this one leads to 0=no room!
    Public NextRoomNorth As Integer = 0
    Public NextRoomSouth As Integer = 0
    Public NextRoomEast As Integer = 0
    Public NextRoomWest As Integer = 0
    'used for text to describe room to player
    Public Desc1 As String
    Public Desc2 As String = ""
    Public Desc3 As String = ""
    Public Desc4 As String = ""
    Public Desc5 As String = ""
    Public Desc6 As String = ""
    Public Desc7 As String = ""
    Public Desc8 As String = ""
    Public Desc9 As String = ""
    Public Desc10 As String = ""
    Public Desc11 As String = ""
    Public Desc12 As String = ""
    Public Desc13 As String = ""
    Public Desc14 As String = ""
    Public Desc15 As String = ""
    Public Desc16 As String = ""
    Public Desc17 As String = ""
    Public Desc18 As String = ""
    Public Desc19 As String = ""
    Public Desc20 As String = ""

    Public Sub Clear()
        'Created 24/07/2024 By Roger Williams
        '
        'resets class variables
        '
        '
        ID = 0
        'disabled 24/07/2024 not needed as new nextroom property does a better job!
        'NorthExit = False
        'SouthExit = False
        'EastExit = False
        'WestExit = False
        NextRoomNorth = 0
        NextRoomSouth = 0
        NextRoomEast = 0
        NextRoomWest = 0
        Desc1 = ""
        Desc2 = ""
        Desc3 = ""
        Desc4 = ""
        Desc5 = ""
        Desc6 = ""
        Desc7 = ""
        Desc8 = ""
        Desc9 = ""
        Desc10 = ""
        Desc11 = ""
        Desc12 = ""
        Desc13 = ""
        Desc14 = ""
        Desc15 = ""
        Desc16 = ""
        Desc17 = ""
        Desc18 = ""
        Desc19 = ""
        Desc20 = ""
    End Sub
End Class
