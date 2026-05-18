using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework.Input;

namespace NonLatinKeyboardFix
{
    public partial class Plugin : IAssemblyPlugin
    {
        public IConfigService ConfigService { get; set; }
        public IPluginManagementService PluginService { get; set; }
        public ILoggerService LoggerService { get; set; }

        private static bool applied;

        public void Initialize()
        {
            ApplyKeyboardMapFix();
        }

        public void OnLoadCompleted()
        {
            ApplyKeyboardMapFix();
        }

        public void PreInitPatching()
        {
        }

        public void Dispose()
        {
            // Nothing to clean up.
        }
        
        // This is a client-side workaround for Barotrauma/MonoGame layouts
        // where SDL layout-dependent keycodes resolve to non-Latin characters
        // and KeyboardUtil.ToXna returns Keys.None.
        // It does not replace the engine-level scancode fallback fix.
        
        private void ApplyKeyboardMapFix()
        {
            if (applied) { return; }

            try
            {
                Type keyboardUtilType = typeof(Keys).Assembly.GetType("Microsoft.Xna.Framework.Input.KeyboardUtil");
                if (keyboardUtilType == null)
                {
                    Log("KeyboardUtil type not found.");
                    return;
                }

                FieldInfo mapField = keyboardUtilType.GetField(
                    "_map",
                    BindingFlags.Static | BindingFlags.NonPublic);

                if (mapField == null)
                {
                    Log("KeyboardUtil._map field not found.");
                    return;
                }

                object value = mapField.GetValue(null);
                Dictionary<int, Keys> map = value as Dictionary<int, Keys>;

                if (map == null)
                {
                    Log("KeyboardUtil._map is not ready yet.");
                    return;
                }

                AddUkrainianLayout(map);
                AddRussianLayout(map);
                AddBelarusianLayout(map);
                AddGreekLayout(map);
                AddBulgarianLayout(map);
                AddSerbianCyrillicLayout(map);
                AddKazakhCyrillicLayout(map);
                AddMongolianCyrillicLayout(map);
                AddMacedonianCyrillicLayout(map);
                AddTajikCyrillicLayout(map);
                AddArmenianPhoneticLayout(map);
                AddGeorgianQwertyLayout(map);

                applied = true;
                Log("Non-Latin keyboard mappings applied.");
            }
            catch (Exception ex)
            {
                Log("Failed to apply keyboard mappings: " + ex);
            }
        }

        private static void AddUkrainianLayout(Dictionary<int, Keys> map)
        {
            // Top row: QWERTYUIOP[]
            Add(map, 'й', Keys.Q);
            Add(map, 'ц', Keys.W);
            Add(map, 'у', Keys.E);
            Add(map, 'к', Keys.R);
            Add(map, 'е', Keys.T);
            Add(map, 'н', Keys.Y);
            Add(map, 'г', Keys.U);
            Add(map, 'ш', Keys.I);
            Add(map, 'щ', Keys.O);
            Add(map, 'з', Keys.P);
            Add(map, 'х', Keys.OemOpenBrackets);
            Add(map, 'ї', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'
            Add(map, 'ф', Keys.A);
            Add(map, 'і', Keys.S);
            Add(map, 'в', Keys.D);
            Add(map, 'а', Keys.F);
            Add(map, 'п', Keys.G);
            Add(map, 'р', Keys.H);
            Add(map, 'о', Keys.J);
            Add(map, 'л', Keys.K);
            Add(map, 'д', Keys.L);
            Add(map, 'ж', Keys.OemSemicolon);
            Add(map, 'є', Keys.OemQuotes);

            // Bottom row: ZXCVBNM,.
            Add(map, 'я', Keys.Z);
            Add(map, 'ч', Keys.X);
            Add(map, 'с', Keys.C);
            Add(map, 'м', Keys.V);
            Add(map, 'и', Keys.B);
            Add(map, 'т', Keys.N);
            Add(map, 'ь', Keys.M);
            Add(map, 'б', Keys.OemComma);
            Add(map, 'ю', Keys.OemPeriod);
        }

        private static void AddRussianLayout(Dictionary<int, Keys> map)
        {
            // Top row: QWERTYUIOP[]
            Add(map, 'й', Keys.Q);
            Add(map, 'ц', Keys.W);
            Add(map, 'у', Keys.E);
            Add(map, 'к', Keys.R);
            Add(map, 'е', Keys.T);
            Add(map, 'н', Keys.Y);
            Add(map, 'г', Keys.U);
            Add(map, 'ш', Keys.I);
            Add(map, 'щ', Keys.O);
            Add(map, 'з', Keys.P);
            Add(map, 'х', Keys.OemOpenBrackets);
            Add(map, 'ъ', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'
            Add(map, 'ф', Keys.A);
            Add(map, 'ы', Keys.S);
            Add(map, 'в', Keys.D);
            Add(map, 'а', Keys.F);
            Add(map, 'п', Keys.G);
            Add(map, 'р', Keys.H);
            Add(map, 'о', Keys.J);
            Add(map, 'л', Keys.K);
            Add(map, 'д', Keys.L);
            Add(map, 'ж', Keys.OemSemicolon);
            Add(map, 'э', Keys.OemQuotes);

            // Bottom row: ZXCVBNM,.
            Add(map, 'я', Keys.Z);
            Add(map, 'ч', Keys.X);
            Add(map, 'с', Keys.C);
            Add(map, 'м', Keys.V);
            Add(map, 'и', Keys.B);
            Add(map, 'т', Keys.N);
            Add(map, 'ь', Keys.M);
            Add(map, 'б', Keys.OemComma);
            Add(map, 'ю', Keys.OemPeriod);
        }

        private static void AddBelarusianLayout(Dictionary<int, Keys> map)
        {
            // Top row: QWERTYUIOP[]
            AddIfMissing(map, 'й', Keys.Q);
            AddIfMissing(map, 'ц', Keys.W);
            AddIfMissing(map, 'у', Keys.E);
            AddIfMissing(map, 'к', Keys.R);
            AddIfMissing(map, 'е', Keys.T);
            AddIfMissing(map, 'н', Keys.Y);
            AddIfMissing(map, 'г', Keys.U);
            AddIfMissing(map, 'ш', Keys.I);
            AddIfMissing(map, 'ў', Keys.O);
            AddIfMissing(map, 'з', Keys.P);
            AddIfMissing(map, 'х', Keys.OemOpenBrackets);

            // Middle row: ASDFGHJKL;'
            AddIfMissing(map, 'ф', Keys.A);
            AddIfMissing(map, 'ы', Keys.S);
            AddIfMissing(map, 'в', Keys.D);
            AddIfMissing(map, 'а', Keys.F);
            AddIfMissing(map, 'п', Keys.G);
            AddIfMissing(map, 'р', Keys.H);
            AddIfMissing(map, 'о', Keys.J);
            AddIfMissing(map, 'л', Keys.K);
            AddIfMissing(map, 'д', Keys.L);
            AddIfMissing(map, 'ж', Keys.OemSemicolon);
            AddIfMissing(map, 'э', Keys.OemQuotes);

            // Bottom row: ZXCVBNM,.
            AddIfMissing(map, 'я', Keys.Z);
            AddIfMissing(map, 'ч', Keys.X);
            AddIfMissing(map, 'с', Keys.C);
            AddIfMissing(map, 'м', Keys.V);
            AddIfMissing(map, 'і', Keys.B);
            AddIfMissing(map, 'т', Keys.N);
            AddIfMissing(map, 'ь', Keys.M);
            AddIfMissing(map, 'б', Keys.OemComma);
            AddIfMissing(map, 'ю', Keys.OemPeriod);
        }

        private static void AddGreekLayout(Dictionary<int, Keys> map)
        {
            // Top row: WERTYUIOP
            AddLowerOnly(map, 'ς', Keys.W);
            Add(map, 'ε', Keys.E);
            Add(map, 'ρ', Keys.R);
            Add(map, 'τ', Keys.T);
            Add(map, 'υ', Keys.Y);
            Add(map, 'θ', Keys.U);
            Add(map, 'ι', Keys.I);
            Add(map, 'ο', Keys.O);
            Add(map, 'π', Keys.P);

            // Middle row: ASDFGHJKL
            Add(map, 'α', Keys.A);
            Add(map, 'σ', Keys.S);
            Add(map, 'δ', Keys.D);
            Add(map, 'φ', Keys.F);
            Add(map, 'γ', Keys.G);
            Add(map, 'η', Keys.H);
            Add(map, 'ξ', Keys.J);
            Add(map, 'κ', Keys.K);
            Add(map, 'λ', Keys.L);

            // Bottom row: ZXCVBNM
            Add(map, 'ζ', Keys.Z);
            Add(map, 'χ', Keys.X);
            Add(map, 'ψ', Keys.C);
            Add(map, 'ω', Keys.V);
            Add(map, 'β', Keys.B);
            Add(map, 'ν', Keys.N);
            Add(map, 'μ', Keys.M);

            // Greek tonos/dialytika characters typed through dead keys.
            Add(map, 'ά', Keys.A);
            Add(map, 'έ', Keys.E);
            Add(map, 'ή', Keys.H);
            Add(map, 'ί', Keys.I);
            Add(map, 'ό', Keys.O);
            Add(map, 'ύ', Keys.Y);
            Add(map, 'ώ', Keys.V);
            Add(map, 'ϊ', Keys.I);
            Add(map, 'ΐ', Keys.I);
            Add(map, 'ϋ', Keys.Y);
            Add(map, 'ΰ', Keys.Y);
        }

        private static void AddBulgarianLayout(Dictionary<int, Keys> map)
        {
            // Top row: QWERTYUIOP[]
            AddIfMissing(map, 'ы', Keys.Q);
            AddIfMissing(map, 'у', Keys.E);
            AddIfMissing(map, 'е', Keys.R);
            AddIfMissing(map, 'и', Keys.T);
            AddIfMissing(map, 'ш', Keys.Y);
            AddIfMissing(map, 'щ', Keys.U);
            AddIfMissing(map, 'к', Keys.I);
            AddIfMissing(map, 'с', Keys.O);
            AddIfMissing(map, 'д', Keys.P);
            AddIfMissing(map, 'з', Keys.OemOpenBrackets);
            AddIfMissing(map, 'ц', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'
            AddIfMissing(map, 'ѝ', Keys.A);
            AddIfMissing(map, 'ь', Keys.S);
            AddIfMissing(map, 'я', Keys.D);
            AddIfMissing(map, 'а', Keys.F);
            AddIfMissing(map, 'о', Keys.G);
            AddIfMissing(map, 'ж', Keys.H);
            AddIfMissing(map, 'г', Keys.J);
            AddIfMissing(map, 'т', Keys.K);
            AddIfMissing(map, 'н', Keys.L);
            AddIfMissing(map, 'в', Keys.OemSemicolon);
            AddIfMissing(map, 'м', Keys.OemQuotes);

            // Bottom row: ZXCVBNM,.
            AddIfMissing(map, 'ю', Keys.Z);
            AddIfMissing(map, 'й', Keys.X);
            AddIfMissing(map, 'ъ', Keys.C);
            AddIfMissing(map, 'э', Keys.V);
            AddIfMissing(map, 'ф', Keys.B);
            AddIfMissing(map, 'х', Keys.N);
            AddIfMissing(map, 'п', Keys.M);
            AddIfMissing(map, 'р', Keys.OemComma);
            AddIfMissing(map, 'л', Keys.OemPeriod);
            AddIfMissing(map, 'б', Keys.OemQuestion);
        }

        private static void AddSerbianCyrillicLayout(Dictionary<int, Keys> map)
        {
            // Top row: QWERTYUIOP[]
            AddIfMissing(map, 'љ', Keys.Q);
            AddIfMissing(map, 'њ', Keys.W);
            AddIfMissing(map, 'е', Keys.E);
            AddIfMissing(map, 'р', Keys.R);
            AddIfMissing(map, 'т', Keys.T);
            AddIfMissing(map, 'з', Keys.Y);
            AddIfMissing(map, 'у', Keys.U);
            AddIfMissing(map, 'и', Keys.I);
            AddIfMissing(map, 'о', Keys.O);
            AddIfMissing(map, 'п', Keys.P);
            AddIfMissing(map, 'ш', Keys.OemOpenBrackets);
            AddIfMissing(map, 'ђ', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'
            AddIfMissing(map, 'а', Keys.A);
            AddIfMissing(map, 'с', Keys.S);
            AddIfMissing(map, 'д', Keys.D);
            AddIfMissing(map, 'ф', Keys.F);
            AddIfMissing(map, 'г', Keys.G);
            AddIfMissing(map, 'х', Keys.H);
            AddIfMissing(map, 'ј', Keys.J);
            AddIfMissing(map, 'к', Keys.K);
            AddIfMissing(map, 'л', Keys.L);
            AddIfMissing(map, 'ч', Keys.OemSemicolon);
            AddIfMissing(map, 'ћ', Keys.OemQuotes);

            // Bottom row: ZXCVBNM
            AddIfMissing(map, 'ѕ', Keys.Z);
            AddIfMissing(map, 'џ', Keys.X);
            AddIfMissing(map, 'ц', Keys.C);
            AddIfMissing(map, 'в', Keys.V);
            AddIfMissing(map, 'б', Keys.B);
            AddIfMissing(map, 'н', Keys.N);
            AddIfMissing(map, 'м', Keys.M);
        }

        private static void AddKazakhCyrillicLayout(Dictionary<int, Keys> map)
        {
            // Number row: 1234567890
            AddIfMissing(map, 'ә', Keys.D1);
            AddIfMissing(map, 'і', Keys.D2);
            AddIfMissing(map, 'ң', Keys.D3);
            AddIfMissing(map, 'ғ', Keys.D4);
            AddIfMissing(map, 'ү', Keys.D7);
            AddIfMissing(map, 'ұ', Keys.D8);
            AddIfMissing(map, 'қ', Keys.D9);
            AddIfMissing(map, 'ө', Keys.D0);

            // Top row: QWERTYUIOP[]
            AddIfMissing(map, 'й', Keys.Q);
            AddIfMissing(map, 'ц', Keys.W);
            AddIfMissing(map, 'у', Keys.E);
            AddIfMissing(map, 'к', Keys.R);
            AddIfMissing(map, 'е', Keys.T);
            AddIfMissing(map, 'н', Keys.Y);
            AddIfMissing(map, 'г', Keys.U);
            AddIfMissing(map, 'ш', Keys.I);
            AddIfMissing(map, 'щ', Keys.O);
            AddIfMissing(map, 'з', Keys.P);
            AddIfMissing(map, 'х', Keys.OemOpenBrackets);
            AddIfMissing(map, 'ъ', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'
            AddIfMissing(map, 'ф', Keys.A);
            AddIfMissing(map, 'ы', Keys.S);
            AddIfMissing(map, 'в', Keys.D);
            AddIfMissing(map, 'а', Keys.F);
            AddIfMissing(map, 'п', Keys.G);
            AddIfMissing(map, 'р', Keys.H);
            AddIfMissing(map, 'о', Keys.J);
            AddIfMissing(map, 'л', Keys.K);
            AddIfMissing(map, 'д', Keys.L);
            AddIfMissing(map, 'ж', Keys.OemSemicolon);
            AddIfMissing(map, 'э', Keys.OemQuotes);

            // Bottom row: ZXCVBNM,.
            AddIfMissing(map, 'я', Keys.Z);
            AddIfMissing(map, 'ч', Keys.X);
            AddIfMissing(map, 'с', Keys.C);
            AddIfMissing(map, 'м', Keys.V);
            AddIfMissing(map, 'и', Keys.B);
            AddIfMissing(map, 'т', Keys.N);
            AddIfMissing(map, 'ь', Keys.M);
            AddIfMissing(map, 'б', Keys.OemComma);
            AddIfMissing(map, 'ю', Keys.OemPeriod);
        }

        private static void AddMongolianCyrillicLayout(Dictionary<int, Keys> map)
        {
            // Mongolian Cyrillic shares many letters with Russian/Ukrainian layouts but places
            // several of them on different physical keys. Add only non-conflicting letters here.
            AddIfMissing(map, 'ү', Keys.O);
            AddIfMissing(map, 'ө', Keys.F);
            AddIfMissing(map, 'ё', Keys.C);
        }

        private static void AddMacedonianCyrillicLayout(Dictionary<int, Keys> map)
        {
            // Top row: QWERTYUIOP[]
            AddIfMissing(map, 'љ', Keys.Q);
            AddIfMissing(map, 'њ', Keys.W);
            AddIfMissing(map, 'е', Keys.E);
            AddIfMissing(map, 'р', Keys.R);
            AddIfMissing(map, 'т', Keys.T);
            AddIfMissing(map, 'ѕ', Keys.Y);
            AddIfMissing(map, 'у', Keys.U);
            AddIfMissing(map, 'и', Keys.I);
            AddIfMissing(map, 'о', Keys.O);
            AddIfMissing(map, 'п', Keys.P);
            AddIfMissing(map, 'ш', Keys.OemOpenBrackets);
            AddIfMissing(map, 'ѓ', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'\.
            AddIfMissing(map, 'а', Keys.A);
            AddIfMissing(map, 'с', Keys.S);
            AddIfMissing(map, 'д', Keys.D);
            AddIfMissing(map, 'ф', Keys.F);
            AddIfMissing(map, 'г', Keys.G);
            AddIfMissing(map, 'х', Keys.H);
            AddIfMissing(map, 'ј', Keys.J);
            AddIfMissing(map, 'к', Keys.K);
            AddIfMissing(map, 'л', Keys.L);
            AddIfMissing(map, 'ч', Keys.OemSemicolon);
            AddIfMissing(map, 'ќ', Keys.OemQuotes);
            AddIfMissing(map, 'ж', Keys.OemPipe);

            // Bottom row: ZXCVBNM
            AddIfMissing(map, 'з', Keys.Z);
            AddIfMissing(map, 'џ', Keys.X);
            AddIfMissing(map, 'ц', Keys.C);
            AddIfMissing(map, 'в', Keys.V);
            AddIfMissing(map, 'б', Keys.B);
            AddIfMissing(map, 'н', Keys.N);
            AddIfMissing(map, 'м', Keys.M);
        }

        private static void AddTajikCyrillicLayout(Dictionary<int, Keys> map)
        {
            // Number row: `-=
            AddIfMissing(map, 'ё', Keys.OemTilde);
            AddIfMissing(map, 'ғ', Keys.OemMinus);
            AddIfMissing(map, 'ӯ', Keys.OemPlus);

            // Top row: QWERTYUIOP[]
            AddIfMissing(map, 'й', Keys.Q);
            AddIfMissing(map, 'қ', Keys.W);
            AddIfMissing(map, 'у', Keys.E);
            AddIfMissing(map, 'к', Keys.R);
            AddIfMissing(map, 'е', Keys.T);
            AddIfMissing(map, 'н', Keys.Y);
            AddIfMissing(map, 'г', Keys.U);
            AddIfMissing(map, 'ш', Keys.I);
            AddIfMissing(map, 'ҳ', Keys.O);
            AddIfMissing(map, 'з', Keys.P);
            AddIfMissing(map, 'х', Keys.OemOpenBrackets);
            AddIfMissing(map, 'ъ', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'
            AddIfMissing(map, 'ф', Keys.A);
            AddIfMissing(map, 'ҷ', Keys.S);
            AddIfMissing(map, 'в', Keys.D);
            AddIfMissing(map, 'а', Keys.F);
            AddIfMissing(map, 'п', Keys.G);
            AddIfMissing(map, 'р', Keys.H);
            AddIfMissing(map, 'о', Keys.J);
            AddIfMissing(map, 'л', Keys.K);
            AddIfMissing(map, 'д', Keys.L);
            AddIfMissing(map, 'ж', Keys.OemSemicolon);
            AddIfMissing(map, 'э', Keys.OemQuotes);

            // Bottom row: ZXCVBNM,.
            AddIfMissing(map, 'я', Keys.Z);
            AddIfMissing(map, 'ч', Keys.X);
            AddIfMissing(map, 'с', Keys.C);
            AddIfMissing(map, 'м', Keys.V);
            AddIfMissing(map, 'и', Keys.B);
            AddIfMissing(map, 'т', Keys.N);
            AddIfMissing(map, 'ӣ', Keys.M);
            AddIfMissing(map, 'б', Keys.OemComma);
            AddIfMissing(map, 'ю', Keys.OemPeriod);
        }

        private static void AddArmenianPhoneticLayout(Dictionary<int, Keys> map)
        {
            // Number row: 1234567890-=
            Add(map, 'է', Keys.D1);
            Add(map, 'թ', Keys.D2);
            Add(map, 'փ', Keys.D3);
            Add(map, 'ձ', Keys.D4);
            Add(map, 'ջ', Keys.D5);
            Add(map, 'ւ', Keys.D6);
            Add(map, 'և', Keys.D7);
            Add(map, 'ր', Keys.D8);
            Add(map, 'չ', Keys.D9);
            Add(map, 'ճ', Keys.D0);
            Add(map, 'ժ', Keys.OemPlus);

            // Top row: QWERTYUIOP[]
            Add(map, 'ք', Keys.Q);
            Add(map, 'ո', Keys.W);
            Add(map, 'ե', Keys.E);
            Add(map, 'ռ', Keys.R);
            Add(map, 'տ', Keys.T);
            Add(map, 'ը', Keys.Y);
            Add(map, 'ւ', Keys.U);
            Add(map, 'ի', Keys.I);
            Add(map, 'օ', Keys.O);
            Add(map, 'պ', Keys.P);
            Add(map, 'խ', Keys.OemOpenBrackets);
            Add(map, 'ծ', Keys.OemCloseBrackets);

            // Middle row: ASDFGHJKL;'\.
            Add(map, 'ա', Keys.A);
            Add(map, 'ս', Keys.S);
            Add(map, 'դ', Keys.D);
            Add(map, 'ֆ', Keys.F);
            Add(map, 'գ', Keys.G);
            Add(map, 'հ', Keys.H);
            Add(map, 'յ', Keys.J);
            Add(map, 'կ', Keys.K);
            Add(map, 'լ', Keys.L);
            Add(map, 'շ', Keys.OemPipe);

            // Bottom row: ZXCVBNM
            Add(map, 'զ', Keys.Z);
            Add(map, 'ղ', Keys.X);
            Add(map, 'ց', Keys.C);
            Add(map, 'վ', Keys.V);
            Add(map, 'բ', Keys.B);
            Add(map, 'ն', Keys.N);
            Add(map, 'մ', Keys.M);
        }

        private static void AddGeorgianQwertyLayout(Dictionary<int, Keys> map)
        {
            // Top row: QWERTYUIOP
            Add(map, 'ქ', Keys.Q);
            Add(map, 'წ', Keys.W);
            Add(map, 'ჭ', Keys.W);
            Add(map, 'ე', Keys.E);
            Add(map, 'რ', Keys.R);
            Add(map, 'ღ', Keys.R);
            Add(map, 'ტ', Keys.T);
            Add(map, 'თ', Keys.T);
            Add(map, 'ყ', Keys.Y);
            Add(map, 'უ', Keys.U);
            Add(map, 'ი', Keys.I);
            Add(map, 'ო', Keys.O);
            Add(map, 'პ', Keys.P);

            // Middle row: ASDFGHJKL
            Add(map, 'ა', Keys.A);
            Add(map, 'ს', Keys.S);
            Add(map, 'შ', Keys.S);
            Add(map, 'დ', Keys.D);
            Add(map, 'ფ', Keys.F);
            Add(map, 'გ', Keys.G);
            Add(map, 'ჰ', Keys.H);
            Add(map, 'ჯ', Keys.J);
            Add(map, 'ჟ', Keys.J);
            Add(map, 'კ', Keys.K);
            Add(map, 'ლ', Keys.L);

            // Bottom row: ZXCVBNM
            Add(map, 'ზ', Keys.Z);
            Add(map, 'ძ', Keys.Z);
            Add(map, 'ხ', Keys.X);
            Add(map, 'ც', Keys.C);
            Add(map, 'ჩ', Keys.C);
            Add(map, 'ვ', Keys.V);
            Add(map, 'ბ', Keys.B);
            Add(map, 'ნ', Keys.N);
            Add(map, 'მ', Keys.M);
        }

        private static void Add(Dictionary<int, Keys> map, char character, Keys key)
        {
            map[(int)character] = key;
            map[(int)char.ToUpperInvariant(character)] = key;
        }

        private static void AddIfMissing(Dictionary<int, Keys> map, char character, Keys key)
        {
            int lower = (int)character;
            int upper = (int)char.ToUpperInvariant(character);

            if (!map.ContainsKey(lower))
            {
                map[lower] = key;
            }

            if (!map.ContainsKey(upper))
            {
                map[upper] = key;
            }
        }

        private static void AddLowerOnly(Dictionary<int, Keys> map, char character, Keys key)
        {
            map[(int)character] = key;
        }

        private void Log(string message)
        {
            try
            {
                LoggerService?.Log("[NonLatinKeyboardFix] " + message);
            }
            catch
            {
                Console.WriteLine("[NonLatinKeyboardFix] " + message);
            }
        }
    }
}
