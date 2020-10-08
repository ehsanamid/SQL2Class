Public Enum Object_Type As Byte
    
    'Hex value is: 00000000
    <Description("Aggregate function (CLR)")>  _
    AF = 0
    
    'Hex value is: 00000001
    <Description("CHECK constraint")>  _
    C = 1
    
    'Hex value is: 00000002
    <Description("DEFAULT (constraint or stand-alone)")>  _
    D = 2
    
    'Hex value is: 00000003
    <Description("FOREIGN KEY constraint")>  _
    F = 3
    
    'Hex value is: 00000004
    <Description("PRIMARY KEY constraint")>  _
    PK = 4
    
    'Hex value is: 00000005
    <Description("SQL stored procedure")>  _
    P = 5
    
    'Hex value is: 00000006
    <Description("Assembly (CLR) stored procedure")>  _
    PC = 6
    
    'Hex value is: 00000007
    <Description("SQL scalar function")>  _
    FN = 7
    
    'Hex value is: 00000008
    <Description("Assembly (CLR) scalar function")>  _
    FS = 8
    
    'Hex value is: 00000009
    <Description("Assembly (CLR) table-valued function")>  _
    FT = 9
    
    'Hex value is: 0000000a
    <Description("Rule (old-style, stand-alone)")>  _
    R = 10
    
    'Hex value is: 0000000b
    <Description("Replication-filter-procedure")>  _
    RF = 11
    
    'Hex value is: 0000000c
    <Description("System base table")>  _
    S = 12
    
    'Hex value is: 0000000d
    <Description("Synonym")>  _
    SN = 13
    
    'Hex value is: 0000000e
    <Description("Service queue")>  _
    SQ = 14
    
    'Hex value is: 0000000f
    <Description("Assembly (CLR) DML trigger")>  _
    TA = 15
    
    'Hex value is: 00000010
    <Description("SQL DML trigger")>  _
    TR = 16
    
    'Hex value is: 00000011
    <Description("SQL inline table-valued function")>  _
    [IF] = 17
    
    'Hex value is: 00000012
    <Description("SQL table-valued-function")>  _
    TF = 18
    
    'Hex value is: 00000013
    <Description("Table (user-defined)")>  _
    U = 19
    
    'Hex value is: 00000014
    <Description("UNIQUE constraint")>  _
    UQ = 20
    
    'Hex value is: 00000015
    <Description("View")>  _
    V = 21
    
    'Hex value is: 00000016
    <Description("Extended stored procedure")>  _
    X = 22
    
    'Hex value is: 00000017
    <Description("Internal table")>  _
    IT = 23
End Enum
