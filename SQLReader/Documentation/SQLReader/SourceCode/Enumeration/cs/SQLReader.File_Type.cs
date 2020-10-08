public enum File_Type : byte {
    
    // Hex value is: 00000000
    [Description("Rows")]
    ROWS = 0,
    
    // Hex value is: 00000001
    [Description("Log")]
    LOG = 1,
    
    // Hex value is: 00000004
    [Description("Full-text")]
    FULLTEXT = 4,
}
