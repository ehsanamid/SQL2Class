Public Enum Snapshot_Isolation_State As Byte
    
    'Hex value is: 00000000
    <Description("Snapshot isolation state is OFF (default). Snapshot isolation is disallowed.")>  _
    OFF = 0
    
    'Hex value is: 00000001
    <Description("Snapshot isolation state ON. Snapshot isolation is allowed.")>  _
    [ON] = 1
    
    'Hex value is: 00000002
    <Description("Snapshot isolation state is in transition to OFF state. All transactions have the"& _ 
        "ir modifications versioned. Cannot start new transactions using snapshot isolati"& _ 
        "on. The database remains in the transition to OFF state until all transactions t"& _ 
        "hat were active when ALTER DATABASE was run can be completed.")>  _
    IN_TRANSITION_TO_ON = 2
    
    'Hex value is: 00000003
    <Description("Snapshot isolation state is in transition to ON state. New transactions have thei"& _ 
        "r modifications versioned. Transactions cannot use snapshot isolation until the "& _ 
        "snapshot isolation state becomes 1 (ON). The database remains in the transition "& _ 
        "to ON state until all update transactions that were active when ALTER DATABASE w"& _ 
        "as run can be completed.")>  _
    IN_TRANSITION_TO_OFF = 3
End Enum
