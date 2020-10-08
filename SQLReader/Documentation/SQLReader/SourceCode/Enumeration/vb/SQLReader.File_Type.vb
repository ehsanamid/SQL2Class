Public Enum File_Type As Byte
    
    'Hex value is: 00000000
    <Description("Rows")>  _
    ROWS = 0
    
    'Hex value is: 00000001
    <Description("Log")>  _
    LOG = 1
    
    'Hex value is: 00000004
    <Description("Full-text")>  _
    FULLTEXT = 4
End Enum
