using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SQLRead
{
    public enum UserTypes : byte
    {
        SQLRole,
        SQLUser,
        NTGroup,
        NTUser
    }

    public enum User_Access : byte
    { 
        MULTI_USER = 0,
        SINGLE_USER = 1,
        RESTRICTED_USER = 2
    }

    public enum State : int
    {
        [Description("Database is available for access. The primary filegroup is online, although the undo phase of recovery may not have been completed.")]
        ONLINE=0,
        [Description("One or more files of the primary filegroup are being restored, or one or more secondary files are being restored offline. The database is unavailable.")]
        RESTORING=1,
        [Description("Database is being recovered. The recovering process is a transient state; the database will automatically become online if the recovery succeeds. If the recovery fails, the database will become suspect. The database is unavailable.")]
        RECOVERING=2,
        [Description("SQL Server has encountered a resource-related error during recovery. The database is not damaged, but files may be missing or system resource limitations may be preventing it from starting. The database is unavailable. Additional action by the user is required to resolve the error and let the recovery process be completed.")]
        RECOVERY_PENDING=3,
        [Description("At least the primary filegroup is suspect and may be damaged. The database cannot be recovered during startup of SQL Server. The database is unavailable. Additional action by the user is required to resolve the problem.")]
        SUSPECT=4,
        [Description("User has changed the database and set the status to EMERGENCY. The database is in single-user mode and may be repaired or restored. The database is marked READ_ONLY, logging is disabled, and access is limited to members of the sysadmin fixed server role. EMERGENCY is primarily used for troubleshooting purposes. For example, a database marked as suspect can be set to the EMERGENCY state. This could permit the system administrator read-only access to the database. Only members of the sysadmin fixed server role can set a database to the EMERGENCY state.")]
        EMERGENCY=5,
        [Description("Database is unavailable. A database becomes offline by explicit user action and remains offline until additional user action is taken. For example, the database may be taken offline in order to move a file to a new disk. The database is then brought back online after the move has been completed.")]
        OFFLINE=6
    }

    public enum FileState : int
    {
        [Description("The file is available for all operations. Files in the primary filegroup are always online if the database itself is online. If a file in the primary filegroup is not online, the database is not online and the states of the secondary files are undefined.")]
        ONLINE = 0,
        [Description("The file is being restored. Files enter the restoring state because of a restore command affecting the whole file, not just a page restore, and remain in this state until the restore is completed and the file is recovered.")]
        RESTORING = 1,
        [Description("The file is being recovered.")]
        RECOVERING = 2,
        [Description("The recovery of the file has been postponed. A file enters this state automatically because of a piecemeal restore process in which the file is not restored and recovered. Additional action by the user is required to resolve the error and allow for the recovery process to be completed. For more information, see Performing Piecemeal Restores.")]
        RECOVERY_PENDING = 3,
        [Description("Recovery of the file failed during an online restore process. If the file is in the primary filegroup, the database is also marked as suspect. Otherwise, only the file is suspect and the database is still online. The file will remain in the suspect state until it is made available by one of the following methods: Restore and recovery or DBCC CHECKDB with REPAIR_ALLOW_DATA_LOSS")]
        SUSPECT = 4,
        //[Description("")]
        //RESERVED FOR FUTURE USE = 5,
        [Description("The file is not available for access and may not be present on the disk. Files become offline by explicit user action and remain offline until additional user action is taken. Caution: A file should only be set offline when the file is corrupted, but it can be restored. A file set to offline can only be set online by restoring the file from backup. For more information about restoring a single file, see RESTORE (Transact-SQL).")]
        OFFLINE = 6,
        [Description("The file was dropped when it was not online. All files in a filegroup become defunct when an offline filegroup is removed.")]
        DEFUNCT = 7
    }

    public enum File_Type : byte 
    { 
        [Description("Rows")]
        ROWS = 0,
        [Description("Log")]
        LOG= 1,
        //[Description("Reserved for future use.")]
        //RESERVED = 2,
        //[Description("Reserved for future use.")]
        //RESERVED = 3,
        [Description("Full-text")]
        FULLTEXT= 4
    }

    [Flags]
    public enum Index_type : byte
    {
        [Description("Heap")]
        Heap = 1,
        [Description("Clustered")]
        Clustered = 2,
        [Description("Nonclustered")]
        Nonclustered = 4,
        [Description("XML")]
        XML = 8,

    }

    public enum Snapshot_Isolation_State : byte
    {
        [Description("Snapshot isolation state is OFF (default). Snapshot isolation is disallowed.")]
        OFF = 0,
        [Description("Snapshot isolation state ON. Snapshot isolation is allowed.")]
        ON = 1,
        [Description("Snapshot isolation state is in transition to OFF state. All transactions have their modifications versioned. Cannot start new transactions using snapshot isolation. The database remains in the transition to OFF state until all transactions that were active when ALTER DATABASE was run can be completed.")]
        IN_TRANSITION_TO_ON = 2,
        [Description("Snapshot isolation state is in transition to ON state. New transactions have their modifications versioned. Transactions cannot use snapshot isolation until the snapshot isolation state becomes 1 (ON). The database remains in the transition to ON state until all update transactions that were active when ALTER DATABASE was run can be completed.")]
        IN_TRANSITION_TO_OFF = 3
    }

    public enum Recovery_Model : byte
    { 
        FULL = 1,
        BULK_LOGGED = 2,
        SIMPLE = 3
    }

    public enum Page_Verify : byte
    { 
        NONE = 0,
        TORN_PAGE_DETECTION = 1,
        CHECKSUM = 2
    }

    public enum Log_Reuse_Wait : byte
    {
        [Description("Currently there are one or more reusable virtual log files.")]
        NOTHING = 0,
        [Description("No checkpoint has occurred since the last log truncation, or the head of the log has not yet moved beyond a virtual log file (all recovery models).This is a routine reason for delaying log truncation. For more information, see Checkpoints and the Active Portion of the Log.")]
        CHECKPOINT = 1,
        [Description("A log backup is required to move the head of the log forward (full or bulk-logged recovery models only). Note:  Log backups do not prevent truncation. When the log backup is completed, the head of the log is moved forward, and some log space might become reusable.")]
        LOG_BACKUP = 2,
        [Description("A data backup or a restore is in progress (all recovery models). A data backup works like an active transaction and, when running, the backup prevents truncation. For more information, see \"Data Backup Operations and Restore Operations,\" later in this topic.")]
        ACTIVE_BACKUP_OR_RESTORE = 3,
        [Description("A transaction is active (all recovery models). A long-running transaction might exist at the start of the log backup. In this case, freeing the space might require another log backup. For more information, see \"Long-Running Active Transactions,\" later in this topic. A transaction is deferred (SQL Server 2005 Enterprise Edition and later versions only). A deferred transaction is effectively an active transaction whose rollback is blocked because of some unavailable resource. For information about the causes of deferred transactions and how to move them out of the deferred state, see Deferred Transactions. ")]
        ACTIVE_TRANSACTION = 4,
        [Description("Database mirroring is paused, or under high-performance mode, the mirror database is significantly behind the principal database (full recovery model only). For more information, see \"Database Mirroring and the Transaction Log,\" later in this topic.")]
        DATABASE_MIRRORING = 5,
        [Description("During transactional replications, transactions relevant to the publications are still undelivered to the distribution database (full recovery model only). For more information, see \"Transactional Replication and the Transaction Log,\" later in this topic.")]
        REPLICATION = 6,
        [Description("A database snapshot is being created (all recovery models). This is a routine, and typically brief, cause of delayed log truncation.")]
        DATABASE_SNAPSHOT_CREATION = 7,
        [Description("A log scan is occurring (all recovery models). This is a routine, and typically brief, cause of delayed log truncation.")]
        LOG_SCAN = 8,
        [Description("This value is currently not used.")]
        OTHER_TRANSIENT = 9
    }

    [Description("The referential action that was declared for this FOREIGN KEY when a delete happens.")]
    public enum Delete_Referential_Action : byte
    {
        [Description("")]
        NO_ACTION = 0,
        [Description("")]
        CASCADE = 1,
        [Description("")]
        SET_NULL = 2,
        [Description("")]
        SET_DEFAULT = 3
    }

    [Description("The referential action that was declared for this FOREIGN KEY when an update happens.")]
    public enum Update_Referential_Action : byte
    {
        [Description("")]
        NO_ACTION = 0,
        [Description("")]
        CASCADE = 1,
        [Description("")]
        SET_NULL = 2,
        [Description("")]
        SET_DEFAULT = 3
    }

    [Description("Indication if the FOREIGN KEY is generated by system or user")]
    public enum Is_System_Named : byte
    {
        [Description("Name was generated by the system.")]
        YES = 1,
        [Description("Name was supplied by the user.")]
        NO = 0

    }

    public enum ExtendedProperty_Type : byte
    {
        [Description("Database")]
        DATABASE = 0,
        [Description("Object or column")]
        OBJECT_OR_COLUMN = 1,
        [Description("Parameter")]
        PARAMETER = 2,
        [Description("Schema")]
        SCHEMA = 3,
        [Description("Database principal")]
        DATABASE_PRINCIPAL = 4,
        [Description("Assembly")]
        ASSEMBLY = 5,
        [Description("Type")]
        TYPE = 6,
        [Description("Index")]
        INDEX = 7,
        [Description("XML schema collection")]
        XML_SCHEMA_COLLECTION = 10,
        [Description("Message type")]
        MESSAGE_TYPE = 15,
        [Description("Service contract")]
        SERVICE_CONTRACT = 16,
        [Description("Service")]
        SERVICE = 17,
        [Description("Remote service binding")]
        REMOTE_SERVICE_BINDING = 18,
        [Description("Route")]
        ROUTE = 19,
        [Description("Dataspace (filegroup or partition scheme)")]
        DATASPACE_FILEGROUP_OR_PARTITION_SCHEME = 20,
        [Description("Partition function")]
        PARTITION_FUNCTION = 21,
        [Description("Database file")]
        DATABASE_FILE = 22
    }

    public enum Object_Type : byte
    {
        [Description("Aggregate function (CLR)")]
        AF = 0,
        [Description("CHECK constraint")]
        C = 1,
        [Description("DEFAULT (constraint or stand-alone)")]
        D = 2,
        [Description("FOREIGN KEY constraint")]
        F = 3,
        [Description("PRIMARY KEY constraint")]
        PK = 4,
        [Description("SQL stored procedure")]
        P = 5,
        [Description("Assembly (CLR) stored procedure")]
        PC = 6,
        [Description("SQL scalar function")]
        FN = 7,
        [Description("Assembly (CLR) scalar function")]
        FS = 8,
        [Description("Assembly (CLR) table-valued function")]
        FT = 9,
        [Description("Rule (old-style, stand-alone)")]
        R = 10,
        [Description("Replication-filter-procedure")]
        RF = 11,
        [Description("System base table")]
        S = 12,
        [Description("Synonym")]
        SN = 13,
        [Description("Service queue")]
        SQ = 14,
        [Description("Assembly (CLR) DML trigger")]
        TA = 15,
        [Description("SQL DML trigger")]
        TR = 16,
        [Description("SQL inline table-valued function")]
        IF = 17,
        [Description("SQL table-valued-function")]
        TF = 18,
        [Description("Table (user-defined)")]
        U = 19,
        [Description("UNIQUE constraint")]
        UQ = 20,
        [Description("View")]
        V = 21,
        [Description("Extended stored procedure")]
        X = 22,
        [Description("Internal table")]
        IT = 23
    }

    public static class Key_Types
    {
        [Description("Aggregate function (CLR)")]
        public const string AF = "AF";
        [Description("CHECK constraint")]
        public const string C = "C";
        [Description("DEFAULT (constraint or stand-alone)")]
        public const string D = "D";
        [Description("FOREIGN KEY constraint")]
        public const string F = "F";
        [Description("PRIMARY KEY constraint")]
        public const string PK = "PK";
        [Description("SQL stored procedure")]
        public const string P = "P";
        [Description("Assembly (CLR) stored procedure")]
        public const string PC = "PC";
        [Description("SQL scalar function")]
        public const string FN = "FN";
        [Description("Assembly (CLR) scalar function")]
        public const string FS = "FS";
        [Description("Assembly (CLR) table-valued function")]
        public const string FT = "FT";
        [Description("Rule (old-style, stand-alone)")]
        public const string R = "R";
        [Description("Replication-filter-procedure")]
        public const string RF = "RF";
        [Description("System base table")]
        public const string S = "S";
        [Description("Synonym")]
        public const string SN = "SN";
        [Description("Service queue")]
        public const string SQ = "SQ";
        [Description("Assembly (CLR) DML trigger")]
        public const string TA = "TA";
        [Description("SQL DML trigger")]
        public const string TR = "TR";
        [Description("SQL inline table-valued function")]
        public const string IF = "IF";
        [Description("SQL table-valued-function")]
        public const string TF = "TF";
        [Description("Table (user-defined)")]
        public const string U = "U";
        [Description("UNIQUE constraint")]
        public const string UQ = "UQ";
        [Description("View")]
        public const string V = "V";
        [Description("Extended stored procedure")]
        public const string X = "X";
        [Description("Internal table")]
        public const string IT = "IT";
    }
}
