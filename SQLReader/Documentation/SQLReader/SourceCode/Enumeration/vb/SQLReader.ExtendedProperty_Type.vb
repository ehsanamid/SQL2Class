Public Enum ExtendedProperty_Type As Byte
    
    'Hex value is: 00000000
    <Description("Database")>  _
    DATABASE = 0
    
    'Hex value is: 00000001
    <Description("Object or column")>  _
    OBJECT_OR_COLUMN = 1
    
    'Hex value is: 00000002
    <Description("Parameter")>  _
    PARAMETER = 2
    
    'Hex value is: 00000003
    <Description("Schema")>  _
    SCHEMA = 3
    
    'Hex value is: 00000004
    <Description("Database principal")>  _
    DATABASE_PRINCIPAL = 4
    
    'Hex value is: 00000005
    <Description("Assembly")>  _
    [ASSEMBLY] = 5
    
    'Hex value is: 00000006
    <Description("Type")>  _
    TYPE = 6
    
    'Hex value is: 00000007
    <Description("Index")>  _
    INDEX = 7
    
    'Hex value is: 0000000a
    <Description("XML schema collection")>  _
    XML_SCHEMA_COLLECTION = 10
    
    'Hex value is: 0000000f
    <Description("Message type")>  _
    MESSAGE_TYPE = 15
    
    'Hex value is: 00000010
    <Description("Service contract")>  _
    SERVICE_CONTRACT = 16
    
    'Hex value is: 00000011
    <Description("Service")>  _
    SERVICE = 17
    
    'Hex value is: 00000012
    <Description("Remote service binding")>  _
    REMOTE_SERVICE_BINDING = 18
    
    'Hex value is: 00000013
    <Description("Route")>  _
    ROUTE = 19
    
    'Hex value is: 00000014
    <Description("Dataspace (filegroup or partition scheme)")>  _
    DATASPACE_FILEGROUP_OR_PARTITION_SCHEME = 20
    
    'Hex value is: 00000015
    <Description("Partition function")>  _
    PARTITION_FUNCTION = 21
    
    'Hex value is: 00000016
    <Description("Database file")>  _
    DATABASE_FILE = 22
End Enum
