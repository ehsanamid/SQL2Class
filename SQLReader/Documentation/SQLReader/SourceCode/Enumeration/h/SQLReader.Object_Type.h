public enum class Object_Type sealed : System::Byte {
    
    // Hex value is: 00000000
    [Description(L"Aggregate function (CLR)")]
    AF = 0,
    
    // Hex value is: 00000001
    [Description(L"CHECK constraint")]
    C = 1,
    
    // Hex value is: 00000002
    [Description(L"DEFAULT (constraint or stand-alone)")]
    D = 2,
    
    // Hex value is: 00000003
    [Description(L"FOREIGN KEY constraint")]
    F = 3,
    
    // Hex value is: 00000004
    [Description(L"PRIMARY KEY constraint")]
    PK = 4,
    
    // Hex value is: 00000005
    [Description(L"SQL stored procedure")]
    P = 5,
    
    // Hex value is: 00000006
    [Description(L"Assembly (CLR) stored procedure")]
    PC = 6,
    
    // Hex value is: 00000007
    [Description(L"SQL scalar function")]
    FN = 7,
    
    // Hex value is: 00000008
    [Description(L"Assembly (CLR) scalar function")]
    FS = 8,
    
    // Hex value is: 00000009
    [Description(L"Assembly (CLR) table-valued function")]
    FT = 9,
    
    // Hex value is: 0000000a
    [Description(L"Rule (old-style, stand-alone)")]
    R = 10,
    
    // Hex value is: 0000000b
    [Description(L"Replication-filter-procedure")]
    RF = 11,
    
    // Hex value is: 0000000c
    [Description(L"System base table")]
    S = 12,
    
    // Hex value is: 0000000d
    [Description(L"Synonym")]
    SN = 13,
    
    // Hex value is: 0000000e
    [Description(L"Service queue")]
    SQ = 14,
    
    // Hex value is: 0000000f
    [Description(L"Assembly (CLR) DML trigger")]
    TA = 15,
    
    // Hex value is: 00000010
    [Description(L"SQL DML trigger")]
    TR = 16,
    
    // Hex value is: 00000011
    [Description(L"SQL inline table-valued function")]
    IF = 17,
    
    // Hex value is: 00000012
    [Description(L"SQL table-valued-function")]
    TF = 18,
    
    // Hex value is: 00000013
    [Description(L"Table (user-defined)")]
    U = 19,
    
    // Hex value is: 00000014
    [Description(L"UNIQUE constraint")]
    UQ = 20,
    
    // Hex value is: 00000015
    [Description(L"View")]
    V = 21,
    
    // Hex value is: 00000016
    [Description(L"Extended stored procedure")]
    X = 22,
    
    // Hex value is: 00000017
    [Description(L"Internal table")]
    IT = 23,
};
