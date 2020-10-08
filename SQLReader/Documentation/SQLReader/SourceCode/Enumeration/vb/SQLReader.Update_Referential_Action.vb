Public Enum Update_Referential_Action As Byte
    
    'Hex value is: 00000000
    NO_ACTION = 0
    
    'Hex value is: 00000001
    CASCADE = 1
    
    'Hex value is: 00000002
    SET_NULL = 2
    
    'Hex value is: 00000003
    SET_DEFAULT = 3
End Enum
