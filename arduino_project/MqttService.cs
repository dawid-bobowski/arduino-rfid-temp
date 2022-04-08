using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace arduino_project
{
    class MqttService
    {
        public static string receivedMessage = "";
        public static string subMessage = "";
        public static string mqttKey = Guid.NewGuid().ToString();
        public void publishMessage(string topic, string content, string ipBroker)
        {
            MqttClient client = new MqttClient(ipBroker);
            client.Connect(Guid.NewGuid().ToString());
            client.Publish(topic, Encoding.Default.GetBytes(content));
            client.Disconnect();
        }
        private void mqttClient_recMessage(object sender, MqttMsgPublishEventArgs e)
        {
            receivedMessage = System.Text.Encoding.Default.GetString(e.Message);
            if (receivedMessage.Length > 0)
            {
                subMessage = receivedMessage;
                this.Invoke(new EventHandler(showMessage));
            }

        }

        private void Invoke(EventHandler eventHandler)
        {
            subMessage = receivedMessage;
        }

        private void showMessage(object sender, EventArgs e)
        {
            subMessage = receivedMessage;
        }
  
        public string subscribeMessage(string temat, string ipBroker)
        {
            MqttClient mqttClient = new MqttClient(ipBroker);
            mqttClient.MqttMsgPublishReceived += mqttClient_recMessage;
            mqttClient.Connect(mqttKey);
            mqttClient.Subscribe(new string[] { temat }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            return subMessage;
        }
    }
}
