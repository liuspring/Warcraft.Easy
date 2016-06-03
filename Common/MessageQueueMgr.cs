using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace Common
{
    public interface IMessageObject
    {
        string MessageQueuePathName { get; }
    }
    /// <summary>
    /// 微软的消息队列MessageQueue的管理类
    /// </summary>
    public class MessageQueueMgr
    {
        /// <summary>
        /// 创建队列(不存在则创建，存在就不创建)
        /// </summary>
        /// <param name="messageQueuePath">队列名称</param>
        public static void CreateMessageQueue(string messageQueuePath)
        {
            if (MessageQueue.Exists(messageQueuePath))
                return;
            MessageQueue.Create(messageQueuePath, false);
            using (var myMessageQueue = new MessageQueue(messageQueuePath))
            {
                myMessageQueue.Label = messageQueuePath.Replace(@".\", "").ToLower();
                myMessageQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
            }
        }

        /// <summary>
        /// 发送消息到队列
        /// </summary>
        /// <typeparam name="T">消息实体对象</typeparam>
        /// <param name="obj">消息实体信息</param>
        public static void SendMessage<T>(T obj) where T : IMessageObject
        {
            if (string.IsNullOrEmpty(obj.MessageQueuePathName)) return;
            using (var myMessageQueue = new MessageQueue(obj.MessageQueuePathName))
            {
                var myMessage = new Message(obj);
                myMessageQueue.Send(myMessage);
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="messageQueuePath"></param>
        public static void SendMessage<T>(T obj, string messageQueuePath)
        {
            if (string.IsNullOrEmpty(messageQueuePath)) return;
            using (var myMessageQueue = new MessageQueue(messageQueuePath))
            {
                var myMessage = new Message(obj);
                myMessageQueue.Send(myMessage);
            }
        }
        /// <summary>
        /// 等待模式提取一个消息
        /// </summary>
        /// <typeparam name="T">消息实体对象</typeparam>
        /// <param name="messageQueuePath">队列的名称</param>
        /// <returns></returns>
        public static T ReceiveMessage<T>(string messageQueuePath)
        {
            using (var myMessageQueue = new MessageQueue(messageQueuePath))
            {
                myMessageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T) });
                var myMessage = myMessageQueue.Receive();
                if (myMessage != null)
                    return (T)myMessage.Body;
                return default(T);
            }
        }
        /// <summary>
        /// 获取所有消息数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageQueuePath"></param>
        /// <returns></returns>
        public static Dictionary<string, T> GetAllMessage<T>(string messageQueuePath)
        {
            Message[] myMessages;
            using (var myMessageQueue = new MessageQueue(messageQueuePath))
            {
                myMessageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T) });
                myMessages = myMessageQueue.GetAllMessages();
            }
            return myMessages.Length > 0 ? myMessages.ToDictionary(a => a.Id, b => (T)b.Body) : null;
        }
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="messageQueuePath"></param>
        public static void RemoveById(string msgId, string messageQueuePath)
        {
            using (var myMessageQueue = new MessageQueue(messageQueuePath))
            {
                myMessageQueue.ReceiveById(msgId);
            }
        }
    }
}
