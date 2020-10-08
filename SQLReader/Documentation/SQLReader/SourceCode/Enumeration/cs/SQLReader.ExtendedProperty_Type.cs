public enum ExtendedProperty_Type : byte {
    
    // Hex value is: 00000000
    [Description("Database")]
    DATABASE = 0,
    
    // Hex value is: 00000001
    [Description("Object or column")]
    OBJECT_OR_COLUMN = 1,
    
    // Hex value is: 00000002
    [Description("Parameter")]
    PARAMETER = 2,
    
    // Hex value is: 00000003
    [Description("Schema")]
    SCHEMA = 3,
    
    // Hex value is: 00000004
    [Description("Database principal")]
    DATABASE_PRINCIPAL = 4,
    
    // Hex value is: 00000005
    [Description("Assembly")]
    ASSEMBLY = 5,
    
    // Hex value is: 00000006
    [Description("Type")]
    TYPE = 6,
    
    // Hex value is: 00000007
    [Description("Index")]
    INDEX = 7,
    
    // Hex value is: 0000000a
    [Description("XML schema collection")]
    XML_SCHEMA_COLLECTION = 10,
    
    // Hex value is: 0000000f
    [Description("Message type")]
    MESSAGE_TYPE = 15,
    
    // Hex value is: 00000010
    [Description("Service contract")]
    SERVICE_CONTRACT = 16,
    
    // Hex value is: 00000011
    [Description("Service")]
    SERVICE = 17,
    
    // Hex value is: 00000012
    [Description("Remote service binding")]
    REMOTE_SERVICE_BINDING = 18,
    
    // Hex value is: 00000013
    [Description("Route")]
    ROUTE = 19,
    
    // Hex value is: 00000014
    [Description("Dataspace (filegroup or partition scheme)")]
    DATASPACE_FILEGROUP_OR_PARTITION_SCHEME = 20,
    
    // Hex value is: 00000015
    [Description("Partition function")]
    PARTITION_FUNCTION = 21,
    
    // Hex value is: 00000016
    [Description("Database file")]
    DATABASE_FILE = 22,
}
