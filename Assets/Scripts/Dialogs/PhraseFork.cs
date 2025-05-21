using System;

namespace Dialogs
{
    public class PhraseFork : Phrase
    {
        public Phrase PhraseB;
        public string ButtonAText;
        public string ButtonBText;

        private int _fork; // 0 - это вариант не выбран, 1 - это вариант А (или 1), 2 - это вариант Б (или 2)

        public override Phrase GetNextPhrase()
        {
            if (_fork == 0)
            {
                throw new System.Exception();
            }

            if (_fork == 1)
            {
                return NextPhrase;
            }
            else
            {
                return PhraseB;
            }
        }

        public void ForkA()
        {
            _fork = 1;
        }

        public void ForkB()
        {
            _fork = 2;
        }
    }
}