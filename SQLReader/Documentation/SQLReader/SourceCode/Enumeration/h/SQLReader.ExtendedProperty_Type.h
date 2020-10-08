public enum class ExtendedProperty_Type sealed : System::Byte {
    
    // Hex value is: 00000000
    [Description(L"Database")]
    DATABASE = 0,
    
    // Hex value is: 00000001
    [Description(L"Object or column")]
    OBJECT_OR_COLUMN = 1,
    
    // Hex value is: 00000002
    [Description(L"Parameter")]
    PARAMETER = 2,
    
    // Hex value is: 00000003
    [Description(L"Schema")]
    SCHEMA = 3,
    
    // Hex value is: 00000004
    [Description(L"Database principal")]
    DATABASE_PRINCIPAL = 4,
    
    // Hex value is: 00000005
    [Description(L"Assembly")]
    ASSEMBLY = 5,
    
    // Hex value is: 00000006
    [Description(L"Type")]
    TYPE = 6,
    
    // Hex value is: 00000007
    [Description(L"Index")]
    INDEX = 7,
    
    // Hex value is: 0000000a
    [Description(L"XML schema collection")]
    XML_SCHEMA_COLLECTION = 10,
    
    // Hex value is: 0000000f
    [Description(L"Message type")]
    MESSAGE_TYPE = 15,
    
    // Hex value is: 00000010
    [Description(L"Service contract")]
    SERVICE_CONTRACT = 16,
    
    // Hex value is: 00000011
    [Description(L"Service")]
    SERVICE = 17,
    
    // Hex value is: 00000012
    [Description(L"Remote service binding")]
    REMOTE_SERVICE_BINDING = 18,
    
    // Hex value is: 00000013
    [Description(L"Route")]
    ROUTE = 19,
    
    // Hex value is: 00000014
    [Description(L"Dataspace (filegroup or partition scheme)")]
    DATASPACE_FILEGROUP_OR_PARTITION_SCHEME = 20,
    
    // Hex value is: 00000015
    [Description(L"Partition function")]
    PARTITION_FUNCTION = 21,
    
    // Hex value is: 00000016
    [Description(L"Database file")]
    DATABASE_FILE = 22,
};
