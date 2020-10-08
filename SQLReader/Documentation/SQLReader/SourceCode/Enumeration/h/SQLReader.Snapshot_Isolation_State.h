public enum class Snapshot_Isolation_State sealed : System::Byte {
    
    // Hex value is: 00000000
    [Description(L"Snapshot isolation state is OFF (default). Snapshot isolation is disallowed.")]
    OFF = 0,
    
    // Hex value is: 00000001
    [Description(L"Snapshot isolation state ON. Snapshot isolation is allowed.")]
    ON = 1,
    
    // Hex value is: 00000002
    [Description(L"Snapshot isolation state is in transition to OFF state. All transactions have the" 
L"ir modifications versioned. Cannot start new transactions using snapshot isolati" 
L"on. The database remains in the transition to OFF state until all transactions t" 
L"hat were active when ALTER DATABASE was run can be completed.")]
    IN_TRANSITION_TO_ON = 2,
    
    // Hex value is: 00000003
    [Description(L"Snapshot isolation state is in transition to ON state. New transactions have thei" 
L"r modifications versioned. Transactions cannot use snapshot isolation until the " 
L"snapshot isolation state becomes 1 (ON). The database remains in the transition " 
L"to ON state until all update transactions that were active when ALTER DATABASE w" 
L"as run can be completed.")]
    IN_TRANSITION_TO_OFF = 3,
};
