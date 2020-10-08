Public Enum Index_type As Byte
    
    'Hex value is: 00000001
    <Description("Heap")>  _
    Heap = 1
    
    'Hex value is: 00000002
    <Description("Clustered")>  _
    Clustered = 2
    
    'Hex value is: 00000004
    <Description("Nonclustered")>  _
    Nonclustered = 4
    
    'Hex value is: 00000008
    <Description("XML")>  _
    XML = 8
End Enum
