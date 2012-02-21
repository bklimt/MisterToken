using System;

namespace MisterToken {
#if WINDOWS || XBOX
    static class Program {
        static void Main(string[] args) {
            using (MisterTokenGame game = new MisterTokenGame()) {
                game.Run();
            }
        }
    }
#endif
}

