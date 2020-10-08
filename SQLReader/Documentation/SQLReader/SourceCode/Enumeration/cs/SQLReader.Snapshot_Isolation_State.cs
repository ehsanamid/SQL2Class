public enum Snapshot_Isolation_State : byte {
    
    // Hex value is: 00000000
    [Description("Snapshot isolation state is OFF (default). Snapshot isolation is disallowed.")]
    OFF = 0,
    
    // Hex value is: 00000001
    [Description("Snapshot isolation state ON. Snapshot isolation is allowed.")]
    ON = 1,
    
    // Hex value is: 00000002
    [Description(@"Snapshot isolation state is in transition to OFF state. All transactions have their modifications versioned. Cannot start new transactions using snapshot isolation. The database remains in the transition to OFF state until all transactions that were active when ALTER DATABASE was run can be completed.")]
    IN_TRANSITION_TO_ON = 2,
    
    // Hex value is: 00000003
    [Description(@"Snapshot isolation state is in transition to ON state. New transactions have their modifications versioned. Transactions cannot use snapshot isolation until the snapshot isolation state becomes 1 (ON). The database remains in the transition to ON state until all update transactions that were active when ALTER DATABASE was run can be completed.")]
    IN_TRANSITION_TO_OFF = 3,
}
