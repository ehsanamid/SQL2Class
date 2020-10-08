Public Enum Recovery_Model As Byte
    
    'Hex value is: 00000001
    FULL = 1
    
    'Hex value is: 00000002
    BULK_LOGGED = 2
    
    'Hex value is: 00000003
    SIMPLE = 3
End Enum
