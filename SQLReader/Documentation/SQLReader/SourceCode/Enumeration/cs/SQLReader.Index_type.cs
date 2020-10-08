public enum Index_type : byte {
    
    // Hex value is: 00000001
    [Description("Heap")]
    Heap = 1,
    
    // Hex value is: 00000002
    [Description("Clustered")]
    Clustered = 2,
    
    // Hex value is: 00000004
    [Description("Nonclustered")]
    Nonclustered = 4,
    
    // Hex value is: 00000008
    [Description("XML")]
    XML = 8,
}
