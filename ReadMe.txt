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
	