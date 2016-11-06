Messaging Patterns
Fire and Forget: 
	client sends message 
	queue confirms receipt 
	handler retrieves message 
	processes work 
	handler confirms message complete
Request/Response: 
	two different queues, client sends request message on one queue and listens for replies on another queue
	client sends message + reply address
	handler retrieves message
	processes work
	sends response message
	client retrieves response message
Publish/Subscribe:
	good for event driven processing
	subscribers register with queue
	publisher sends message
	queue confirms receipt
	queue forwards message to subscribers
	for MSMQ, create regular queue, enter Multicast virtual address, give ANONYMOUS LOGON Receive Message and Peek Message access
		
Handling Failures
Retries:
	Handler 1 retrieves message 1
	Queue locks message 1 but does not remove it
	Handler 1 confirms complete
	Queue removes message 1
	Hander 2 retrieves message 2

	Handler 1 retrieves message 1
	Queue locks message 1 but does not remove it
	Handler 1 confirms failure or does not confirm complete in time
	Queue unlocks message 1
	Hander 2 retrieves message 1
Poison Messages:
	Handler 1 retrieves bad message
	Queue locks bad message but does not remove it
	Handler 1 confirms failure
	Queue unlocks bad message
	Hander 2 retrieves bad message
	Message 2 never processed
Dead Letter Queue:
Message queue systems use DLQ, not via a message handler. MSMQ if a message times out, WebSphere MQ if the destination queue doesn't exists
	Handler 1 retrieves bad message
	Queue locks bad message but does not remove it
	Handler 1 detects poison message, moves to dead-letter queue
	Queue removes bad message
	Hander 2 retrieves message 2

Use transactional queues for durability. Messages must use the transactional API to send and receive items in a transactional queue

MSMQ:
	Installed as a windows service
	Message queuing built into Windows and Windows Server
	Works with Active Directory for security & discoverability
	Core pattern support & advanced features
	Durability & Reliability, messages not persisted by default, use the recoverable flag to save to disk or create the queue as transactional
		Recoverable messages saved to disk, as reliable as your disk/RAID/SAN
		Standard disk storage, not standard back up and restore
		Clustered MSMQ server instances, for redundancy, resilient to machine failure
	Store and forward, persist local; forward; persist remote
	Enable the Journal option in development, useful for auditing, keeps a copy of messages in a seperate journal queue
	Set privacy level (None, Optional, Body) for encryption options
	Poison Message Handling: in version 4, MSMQ added a retry queue between main and dead-letter queue. Messages that couldn't be processed are moved to the
		retry queue until specified time period (RetryCycleDelay) where it is then moved back to the main queue to be processed. This process can repeat
		a number of times (MaxRetryCycles). Different strategies include: discard, return to front (default), return to back, move to other queue, shuttle between queues
	Acknowledgement types
		Positive - reaches destination or read
		Negative - failed to send or be read
	Public Queues:
		Published in Active Directory
		Can query AD to find queues
		Integrates with Windows security
	Private Queues:
		Not integrated with AD
		But still available for public use
		Security features not available
		More commonly used, no AD means better performance
		Can be configured for HTTP support across the internet
		Can reach it through two paths:
			PATH: 
				{machine}\private$\{queueName}
				.\private$\unsubscribe
				SC-MQ-01\private$\unsubscribe
			DIRECT:
				DIRECT={protocol}:{address}\private$\{queueName}
				DIRECT=TCP:192.168.2.140\private$\unsubscribe
				DIRECT=OS:SC-MQ-01\private$\unsubscribe
	