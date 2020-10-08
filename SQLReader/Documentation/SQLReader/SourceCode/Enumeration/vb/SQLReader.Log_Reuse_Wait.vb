Public Enum Log_Reuse_Wait As Byte
    
    'Hex value is: 00000000
    <Description("Currently there are one or more reusable virtual log files.")>  _
    [NOTHING] = 0
    
    'Hex value is: 00000001
    <Description("No checkpoint has occurred since the last log truncation, or the head of the log "& _ 
        "has not yet moved beyond a virtual log file (all recovery models).This is a rout"& _ 
        "ine reason for delaying log truncation. For more information, see Checkpoints an"& _ 
        "d the Active Portion of the Log.")>  _
    CHECKPOINT = 1
    
    'Hex value is: 00000002
    <Description("A log backup is required to move the head of the log forward (full or bulk-logged"& _ 
        " recovery models only). Note:  Log backups do not prevent truncation. When the l"& _ 
        "og backup is completed, the head of the log is moved forward, and some log space"& _ 
        " might become reusable.")>  _
    LOG_BACKUP = 2
    
    'Hex value is: 00000003
    <Description("A data backup or a restore is in progress (all recovery models). A data backup wo"& _ 
        "rks like an active transaction and, when running, the backup prevents truncation"& _ 
        ". For more information, see ""Data Backup Operations and Restore Operations,"" lat"& _ 
        "er in this topic.")>  _
    ACTIVE_BACKUP_OR_RESTORE = 3
    
    'Hex value is: 00000004
    <Description("A transaction is active (all recovery models). A long-running transaction might e"& _ 
        "xist at the start of the log backup. In this case, freeing the space might requi"& _ 
        "re another log backup. For more information, see ""Long-Running Active Transactio"& _ 
        "ns,"" later in this topic. A transaction is deferred (SQL Server 2005 Enterprise "& _ 
        "Edition and later versions only). A deferred transaction is effectively an activ"& _ 
        "e transaction whose rollback is blocked because of some unavailable resource. Fo"& _ 
        "r information about the causes of deferred transactions and how to move them out"& _ 
        " of the deferred state, see Deferred Transactions. ")>  _
    ACTIVE_TRANSACTION = 4
    
    'Hex value is: 00000005
    <Description("Database mirroring is paused, or under high-performance mode, the mirror database"& _ 
        " is significantly behind the principal database (full recovery model only). For "& _ 
        "more information, see ""Database Mirroring and the Transaction Log,"" later in thi"& _ 
        "s topic.")>  _
    DATABASE_MIRRORING = 5
    
    'Hex value is: 00000006
    <Description("During transactional replications, transactions relevant to the publications are "& _ 
        "still undelivered to the distribution database (full recovery model only). For m"& _ 
        "ore information, see ""Transactional Replication and the Transaction Log,"" later "& _ 
        "in this topic.")>  _
    REPLICATION = 6
    
    'Hex value is: 00000007
    <Description("A database snapshot is being created (all recovery models). This is a routine, an"& _ 
        "d typically brief, cause of delayed log truncation.")>  _
    DATABASE_SNAPSHOT_CREATION = 7
    
    'Hex value is: 00000008
    <Description("A log scan is occurring (all recovery models). This is a routine, and typically b"& _ 
        "rief, cause of delayed log truncation.")>  _
    LOG_SCAN = 8
    
    'Hex value is: 00000009
    <Description("This value is currently not used.")>  _
    OTHER_TRANSIENT = 9
End Enum
