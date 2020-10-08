public ref class Key_Types sealed abstract : public System::Object {
    
    public: [Description(L"Aggregate function (CLR)")]
    literal System::String^  AF = L"AF";
    
    public: [Description(L"CHECK constraint")]
    literal System::String^  C = L"C";
    
    public: [Description(L"DEFAULT (constraint or stand-alone)")]
    literal System::String^  D = L"D";
    
    public: [Description(L"FOREIGN KEY constraint")]
    literal System::String^  F = L"F";
    
    public: [Description(L"PRIMARY KEY constraint")]
    literal System::String^  PK = L"PK";
    
    public: [Description(L"SQL stored procedure")]
    literal System::String^  P = L"P";
    
    public: [Description(L"Assembly (CLR) stored procedure")]
    literal System::String^  PC = L"PC";
    
    public: [Description(L"SQL scalar function")]
    literal System::String^  FN = L"FN";
    
    public: [Description(L"Assembly (CLR) scalar function")]
    literal System::String^  FS = L"FS";
    
    public: [Description(L"Assembly (CLR) table-valued function")]
    literal System::String^  FT = L"FT";
    
    public: [Description(L"Rule (old-style, stand-alone)")]
    literal System::String^  R = L"R";
    
    public: [Description(L"Replication-filter-procedure")]
    literal System::String^  RF = L"RF";
    
    public: [Description(L"System base table")]
    literal System::String^  S = L"S";
    
    public: [Description(L"Synonym")]
    literal System::String^  SN = L"SN";
    
    public: [Description(L"Service queue")]
    literal System::String^  SQ = L"SQ";
    
    public: [Description(L"Assembly (CLR) DML trigger")]
    literal System::String^  TA = L"TA";
    
    public: [Description(L"SQL DML trigger")]
    literal System::String^  TR = L"TR";
    
    public: [Description(L"SQL inline table-valued function")]
    literal System::String^  IF = L"IF";
    
    public: [Description(L"SQL table-valued-function")]
    literal System::String^  TF = L"TF";
    
    public: [Description(L"Table (user-defined)")]
    literal System::String^  U = L"U";
    
    public: [Description(L"UNIQUE constraint")]
    literal System::String^  UQ = L"UQ";
    
    public: [Description(L"View")]
    literal System::String^  V = L"V";
    
    public: [Description(L"Extended stored procedure")]
    literal System::String^  X = L"X";
    
    public: [Description(L"Internal table")]
    literal System::String^  IT = L"IT";
};
