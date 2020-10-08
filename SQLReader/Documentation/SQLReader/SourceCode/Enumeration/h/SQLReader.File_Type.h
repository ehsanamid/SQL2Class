public enum class File_Type sealed : System::Byte {
    
    // Hex value is: 00000000
    [Description(L"Rows")]
    ROWS = 0,
    
    // Hex value is: 00000001
    [Description(L"Log")]
    LOG = 1,
    
    // Hex value is: 00000004
    [Description(L"Full-text")]
    FULLTEXT = 4,
};
