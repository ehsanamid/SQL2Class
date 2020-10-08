public enum Log_Reuse_Wait : byte {
    
    // Hex value is: 00000000
    [Description("Currently there are one or more reusable virtual log files.")]
    NOTHING = 0,
    
    // Hex value is: 00000001
    [Description(@"No checkpoint has occurred since the last log truncation, or the head of the log has not yet moved beyond a virtual log file (all recovery models).This is a routine reason for delaying log truncation. For more information, see Checkpoints and the Active Portion of the Log.")]
    CHECKPOINT = 1,
    
    // Hex value is: 00000002
    [Description(@"A log backup is required to move the head of the log forward (full or bulk-logged recovery models only). Note:  Log backups do not prevent truncation. When the log backup is completed, the head of the log is moved forward, and some log space might become reusable.")]
    LOG_BACKUP = 2,
    
    // Hex value is: 00000003
    [Description(@"A data backup or a restore is in progress (all recovery models). A data backup works like an active transaction and, when running, the backup prevents truncation. For more information, see ""Data Backup Operations and Restore Operations,"" later in this topic.")]
    ACTIVE_BACKUP_OR_RESTORE = 3,
    
    // Hex value is: 00000004
    [Description(@"A transaction is active (all recovery models). A long-running transaction might exist at the start of the log backup. In this case, freeing the space might require another log backup. For more information, see ""Long-Running Active Transactions,"" later in this topic. A transaction is deferred (SQL Server 2005 Enterprise Edition and later versions only). A deferred transaction is effectively an active transaction whose rollback is blocked because of some unavailable resource. For information about the causes of deferred transactions and how to move them out of the deferred state, see Deferred Transactions. ")]
    ACTIVE_TRANSACTION = 4,
    
    // Hex value is: 00000005
    [Description("Database mirroring is paused, or under high-performance mode, the mirror database" +
        " is significantly behind the principal database (full recovery model only). For " +
        "more information, see \"Database Mirroring and the Transaction Log,\" later in thi" +
        "s topic.")]
    DATABASE_MIRRORING = 5,
    
    // Hex value is: 00000006
    [Description("During transactional replications, transactions relevant to the publications are " +
        "still undelivered to the distribution database (full recovery model only). For m" +
        "ore information, see \"Transactional Replication and the Transaction Log,\" later " +
        "in this topic.")]
    REPLICATION = 6,
    
    // Hex value is: 00000007
    [Description("A database snapshot is being created (all recovery models). This is a routine, an" +
        "d typically brief, cause of delayed log truncation.")]
    DATABASE_SNAPSHOT_CREATION = 7,
    
    // Hex value is: 00000008
    [Description("A log scan is occurring (all recovery models). This is a routine, and typically b" +
        "rief, cause of delayed log truncation.")]
    LOG_SCAN = 8,
    
    // Hex value is: 00000009
    [Description("This value is currently not used.")]
    OTHER_TRANSIENT = 9,
}
