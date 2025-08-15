
    public static class DemoMode
    {
        public static bool On =>
#if DEMO
            true;
#else
        false;
#endif
    }

