public enum Object_Type : byte {
    
    // Hex value is: 00000000
    [Description("Aggregate function (CLR)")]
    AF = 0,
    
    // Hex value is: 00000001
    [Description("CHECK constraint")]
    C = 1,
    
    // Hex value is: 00000002
    [Description("DEFAULT (constraint or stand-alone)")]
    D = 2,
    
    // Hex value is: 00000003
    [Description("FOREIGN KEY constraint")]
    F = 3,
    
    // Hex value is: 00000004
    [Description("PRIMARY KEY constraint")]
    PK = 4,
    
    // Hex value is: 00000005
    [Description("SQL stored procedure")]
    P = 5,
    
    // Hex value is: 00000006
    [Description("Assembly (CLR) stored procedure")]
    PC = 6,
    
    // Hex value is: 00000007
    [Description("SQL scalar function")]
    FN = 7,
    
    // Hex value is: 00000008
    [Description("Assembly (CLR) scalar function")]
    FS = 8,
    
    // Hex value is: 00000009
    [Description("Assembly (CLR) table-valued function")]
    FT = 9,
    
    // Hex value is: 0000000a
    [Description("Rule (old-style, stand-alone)")]
    R = 10,
    
    // Hex value is: 0000000b
    [Description("Replication-filter-procedure")]
    RF = 11,
    
    // Hex value is: 0000000c
    [Description("System base table")]
    S = 12,
    
    // Hex value is: 0000000d
    [Description("Synonym")]
    SN = 13,
    
    // Hex value is: 0000000e
    [Description("Service queue")]
    SQ = 14,
    
    // Hex value is: 0000000f
    [Description("Assembly (CLR) DML trigger")]
    TA = 15,
    
    // Hex value is: 00000010
    [Description("SQL DML trigger")]
    TR = 16,
    
    // Hex value is: 00000011
    [Description("SQL inline table-valued function")]
    IF = 17,
    
    // Hex value is: 00000012
    [Description("SQL table-valued-function")]
    TF = 18,
    
    // Hex value is: 00000013
    [Description("Table (user-defined)")]
    U = 19,
    
    // Hex value is: 00000014
    [Description("UNIQUE constraint")]
    UQ = 20,
    
    // Hex value is: 00000015
    [Description("View")]
    V = 21,
    
    // Hex value is: 00000016
    [Description("Extended stored procedure")]
    X = 22,
    
    // Hex value is: 00000017
    [Description("Internal table")]
    IT = 23,
}
