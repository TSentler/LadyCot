using UnityEngine;
using UnityEngine.Events;

namespace Dialogs
{
    public class Phrase : MonoBehaviour
    {
        public string Name;
        public string Message;
        public Camera Camera;
        public int BackgroundIndex = -1;
        public Phrase NextPhrase;

        public virtual Phrase GetNextPhrase()
        {
            return NextPhrase;
        }
    }
}