@echo off

start "Unsubscribe Handler" Sixeyed.MessageQueue.Handler\bin\debug\Sixeyed.MessageQueue.Handler.exe

start "DoesUserExist Handler" Sixeyed.MessageQueue.Handler\bin\debug\Sixeyed.MessageQueue.Handler.exe .\private$\sixeyed.messagequeue.doesuserexist

start "Unsubscribe Legacy Handler" Sixeyed.MessageQueue.Handler.UnsubscribeLegacy\bin\debug\Sixeyed.MessageQueue.Handler.UnsubscribeLegacy.exe

start "Unsubscribe CRM Handler" Sixeyed.MessageQueue.Handler.UnsubscribeCrm\bin\debug\Sixeyed.MessageQueue.Handler.UnsubscribeCrm.exe

start "Unsubscribe Fulfillment Handler" Sixeyed.MessageQueue.Handler.UnsubscribeFulfill\bin\debug\Sixeyed.MessageQueue.Handler.UnsubscribeFulfill.exe