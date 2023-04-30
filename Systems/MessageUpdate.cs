using System;
using RLNET;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSharp1.Systems
{
    public class MessageUpdate
    {
        private static readonly int maxLines = 10;
        private readonly Queue<string> messages;
        public MessageUpdate()
        {
            messages = new Queue<string>();
        }
        public void AddMessage(string message)
        {
            messages.Enqueue(message);
            
            if(messages.Count == maxLines)
            {
                messages.Dequeue();
            }
        }
        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] messagesArray = messages.ToArray();
            for(int i = 0; i < messagesArray.Length; i++)
            {
                console.Print(1, i + 1, messagesArray[i], RLColor.White);
            }
        }
    }
}
