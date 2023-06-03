using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System.Globalization;
using System.Text.RegularExpressions;

public class bakery : MonoBehaviour
{
    public new KMAudio audio;
    public KMBombInfo bomb;
    public KMBombModule module;

    public KMSelectable[] plates;
    public KMSelectable chalkboard;
    private Renderer[] plateRenders;
    public Renderer[] cookieRenders;
    public TextMesh chalkboardText;
    public GameObject rollingPin;
    public Color plateColor;
    public Color highlightColor;
    public Color purpleColor;

    public Texture[] regularCookieTextures;
    public Texture[] teaBiscuitTextures;
    public Texture[] chocolateButterBiscuitTextures;
    public Texture[] brandedTextures;
    public Texture[] danishButterCookieTextures;
    public Texture[] macaronTextures;
    public Texture[] notCookieTextures;
    public Texture[] seasonalCookieTextures;

    private static string[] regularCookieNames = new string[140] { "Chocolate chip cookie", "Plain cookie", "Sugar cookie", "Oatmeal raisin cookie", "Peanut butter cookie", "Coconut cookie", "Almond cookie", "Hazelnut cookie", "Walnut cookie", "Cashew cookie", "White chocolate cookie", "Milk chocolate cookie", "Macadamia nut cookie", "Double-chip cookie", "White chocolate macadamia nut cookie", "All-chocolate cookie", "Dark chocolate-coated cookie", "White chocolate-coated cookie", "Eclipse cookie", "Zebra cookie", "Snickerdoodle", "Stroopwafel", "Macaroon", "Empire biscuit", "Madeleine", "Palmier", "Palet", "Sablé", "Pure black chocolate cookie", "Pure white chocolate cookie", "Ladyfinger", "Tuile", "Chocolate-stuffed biscuit", "Checker cookie", "Butter cookie", "Cream cookie", "Gingersnap", "Cinnamon cookie", "Vanity cookie", "Cigar", "Pinwheel cookie", "Shortbread biscuit", "Millionaires' shortbread", "Caramel cookie", "Pecan sandy", "Moravian spice cookie", "Anzac biscuit", "Buttercake", "Pink biscuit", "Whole grain cookie", "Candy cookie", "Big chip cookie", "One chip cookie", "Sprinkles cookie", "Peanut butter blossom", "No-bake cookie", "Florentine", "Chocolate crinkle", "Maple cookie", "Persian rice cookie", "Norwegian cookie", "Crispy rice cookie", "Ube cookie", "Butterscotch cookie", "Speculaa", "Chocolate oatmeal cookie", "Molasses cookie", "Biscotti", "Waffle cookie", "Bourbon biscuit", "Mini-cookies", "Whoopie pie", "Caramel wafer biscuit", "Chocolate chip mocha cookie", "Earl Grey cookie", "Chai tea cookie", "Corn syrup cookie", "Icebox cookies", "Graham crackers", "Hardtack", "Cornflake cookie", "Deep-fried cookie dough", "Gluten-free cookie", "Russian bread cookie", "Lebkuchen", "Aachener Printen", "Canistrelli", "Nice biscuit", "French pure butter cookie", "Petit buerre", "Nanaimo bar", "Berger cookie", "Chinsuko", "Panda koala biscuits", "Putri salju", "Milk cookie", "Kruidnoten", "Marie biscuit", "Meringue cookie", "Yogurt cookie", "Thumbprint cookie", "Pizzelle", "Granola cookie", "Ricotta cookie", "Roze koeken", "Peanut butter cup cookie", "Sesame cookie", "Taiyaki", "Vanillekipferl", "Battenberg biscuit", "Rosette cookie", "Gangmaker", "Welsh cookie", "Raspberry cheesecake cookie", "Bokkenpootje", "Fat rascal", "Dalgona cookies", "Bakeberry cookie", "Duketater cookie", "Smile cookie", "Wheat slim", "Ischler cookies", "Custard cream", "Matcha cookie", "Kolachy cookie", "Gomma cookie", "Coyota", "Frosted sugar cookies", "Marshmallow sandwich cookie", "Web cookie", "Havreflarn", "Alfajore", "Gaufrette", "Cookie bar", "Snowball cookie", "Sequilho", "Hazelnut swirly", "Spritz cookies", "Mbtata cookie", "Springerle" };
    private static string[] teaBiscuitNames = new string[6] { "Tea biscuit", "Chocolate tea biscuit", "Round tea biscuit", "Chocolate round tea biscuit", "Hearted tea biscuit", "Chocolate hearted tea biscuit" };
    private static string[] chocolateButterBiscuitNames = new string[12] { "Milk chocolate butter biscuit", "Dark chocolate butter biscuit", "White chocolate butter biscuit", "Ruby chocolate butter biscuit", "Lavender chocolate butter biscuit", "Synthetic chocolate green honey butter biscuit", "Royal raspberry chocolate butter biscuit", "Ultra-concentrated high-energy chocolate butter biscuit", "Pure pitch-black chocolate butter biscuit", "Cosmic chocolate butter biscuit", "Butter biscuit (with butter)", "Everybutter biscuit" };
    private static string[] brandedNames = new string[16] { "Caramoa", "Sagalong", "Shortfoil", "Win mint", "Fig glutton", "Loreol", "Jaffa cake", "Grease's cup", "Digits", "Lombardia cookie", "Bastenaken cookie", "Festivity loops", "Havabreaks", "Zilla wafers", "Dim dam", "Pokey" };
    private static string[] danishButterCookieNames = new string[5] { "Butter horseshoe", "Butter puck", "Butter knot", "Butter slab", "Butter swirl" };
    private static string[] macaronNames = new string[9] { "Rose macaron", "Lemon macaron", "Chocolate macaron", "Pistachio macaron", "Hazelnut macaron", "Violet macaron", "Caramel macaron", "Licorice macaron", "Earl Grey macaron" };
    private static string[] notCookieNames = new string[12] { "Apple pie", "Butter croissant", "Ice cream sandwich", "Jelly donut", "Chocolate cake", "Crackers", "Lemon meringue pie", "Profiteroles", "Fudge square", "Glazed donut", "Strawberry cake", "Toast" };
    private static string[] seasonalCookieNames = new string[21] { "Bell cookie", "Candy cane cookie", "Christmas tree cookie", "Holly cookie", "Present cookie", "Snowflake cookie", "Snowman cookie", "Bat cookie", "Eyeball cookie", "Ghost cookie", "Pumpkin cookie", "Skull cookie", "Slime cookie", "Spider cookie", "Pure heart cookie", "Ardent heart cookie", "Sour heart cookie", "Weeping heart cookie", "Golden heart cookie", "Eternal heart cookie", "Prism heart cookie" };
    private static string[] coordinates = new string[12] { "A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3" };

    private int[] cookieIndices = new int[12];
    private CookieCategory[] allCookieCategories = new CookieCategory[12];
    private string[] allCookieNames = new string[12];
    private bool[] solution = new bool[12];
    private bool[] selected = new bool[12];
    private int lastHighlightedButton = -1;

    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;

    #region ModSettings
    bakerySettings Settings = new bakerySettings();
#pragma warning disable 414
    private static Dictionary<string, object>[] TweaksEditorSettings = new Dictionary<string, object>[]
    {
      new Dictionary<string, object>
      {
        { "Filename", "Bakery Settings.json"},
        { "Name", "Bakery" },
        { "Listings", new List<Dictionary<string, object>>
        {
          new Dictionary<string, object>
          {
            { "Key", "HardMode" },
            { "Text", "Enable hard mode (names will not be displayed)?"}
          }
        }}
      }
    };
#pragma warning restore 414

    private class bakerySettings
    {
        public bool HardMode = false;
    }
    #endregion

    private void Awake()
    {
        moduleId = moduleIdCounter++;
        var modConfig = new modConfig<bakerySettings>("Bakery Settings");
        Settings = modConfig.read();

        var missionDesc = KTMissionGetter.Mission.Description;
        if (missionDesc != null)
        {
            var regex = new Regex(@"\[Bakery\] (true|false)");
            var match = regex.Match(missionDesc);
            if (match.Success)
            {
                string[] options = match.Value.Replace("[Bakery] ", "").Split(',');
                bool[] values = new bool[options.Length];
                for (int i = 0; i < options.Length; i++)
                    values[i] = options[i] == "true" ? true : false;
                Settings.HardMode = values[0];
            }
        }
        plateRenders = plates.Select(x => x.GetComponent<Renderer>()).ToArray();
        foreach (KMSelectable plate in plates)
        {
            var ix = Array.IndexOf(plates, plate);
            plate.OnInteract += delegate () { PressPlate(ix); return false; };
            plate.OnHighlight += delegate ()
            {
                if (lastHighlightedButton == ix)
                    return;
                lastHighlightedButton = ix;
                plateRenders[ix].material.color = highlightColor;
                SetHoverText(Settings.HardMode ? "???" : allCookieNames[ix]);
            };
            plate.OnHighlightEnded += delegate ()
            {
                plateRenders[ix].material.color = !selected[ix] ? plateColor : purpleColor;
                chalkboardText.text = "";
            };
        }
        chalkboard.OnInteract += delegate () { Submit(); return false; };
        modConfig.write(Settings);
    }

    private void Start()
    {
        var allSquares = new string[35][];
        for (int i = 0; i < 35; i++)
            allSquares[i] = regularCookieNames.Skip(4 * i).Take(4).ToArray();
        var probabilities = new CookieCategory[] { CookieCategory.regular, CookieCategory.regular, CookieCategory.regular, CookieCategory.regular, CookieCategory.regular, CookieCategory.regular, CookieCategory.regular, CookieCategory.teaBiscuit, CookieCategory.branded, CookieCategory.chocolateButterBiscuit, CookieCategory.danishButter, CookieCategory.macaron, CookieCategory.notCookie, CookieCategory.seasonal };
        var maxCounts = new int[] { 140, 6, 12, 16, 5, 9, 12, 21 };
        var pointIndices = new int[][]
        {
            new int[8] { -1, -1, 1, 5, 4, -1, -1, -1 },
            new int[8] { -1, -1, 2, 6, 5, 4, 0, -1 },
            new int[8] { -1, -1, 3, 7, 6, 5, 1, -1 },
            new int[8] {-1, -1, -1, -1, 7, 6, 2, -1 },
            new int[8] { 0, 1, 5, 9, 8, -1, -1, -1 },
            new int[8] { 1, 2, 6, 10, 9, 8, 4, 0 },
            new int[8] { 2, 3, 7, 11, 10, 9, 5, 1 },
            new int[8] { 3, -1, -1, -1, 11, 10, 6, 2 },
            new int[8] { 4, 5, 9, -1, -1, -1, -1, -1 },
            new int[8] { 5, 6, 10, -1, -1, -1, -1, 4 },
            new int[8] { 6, 7, 11, -1, -1, -1, -1, 5 },
            new int[8] { 7, -1, -1, -1, -1, -1, 10, 6 }
         };
        var adjacentIndices = new int[][]
        {
            new int[] { 1, 4 },
            new int[] { 0, 2, 5 },
            new int[] { 1, 3, 6 },
            new int[] { 2, 7 },
            new int[] { 0, 5, 8 },
            new int[] { 1, 4, 6, 9 },
            new int[] { 2, 5, 7, 10 },
            new int[] { 3, 6, 11 },
            new int[] { 4, 9 },
            new int[] { 5, 8, 10 },
            new int[] { 6, 9, 11 },
            new int[] { 7, 10}
        };
        var loggingStrings = new List<string>();
    tryAgain:

    // Assignment
    oopsyDaisy:
        cookieIndices = Enumerable.Repeat(-1, 12).ToArray();
        allCookieCategories = Enumerable.Repeat(CookieCategory.notSet, 12).ToArray();
        for (int i = 0; i < 12; i++)
        {
            var thisCategory = probabilities.PickRandom();
            if (allCookieCategories.Count(x => x == thisCategory) == maxCounts[(int)thisCategory])
                goto oopsyDaisy;
            allCookieCategories[i] = thisCategory;

            switch (thisCategory)
            {
                case CookieCategory.regular:
                    cookieIndices[i] = Enumerable.Range(0, 140).Where(x => !cookieIndices.Where((_, ix) => allCookieCategories[ix] == CookieCategory.regular).Contains(x)).PickRandom();
                    allCookieNames[i] = regularCookieNames[cookieIndices[i]];
                    cookieRenders[i].material.mainTexture = regularCookieTextures[cookieIndices[i]];
                    break;
                case CookieCategory.teaBiscuit:
                    cookieIndices[i] = Enumerable.Range(0, 6).Where(x => !cookieIndices.Where((_, ix) => allCookieCategories[ix] == CookieCategory.teaBiscuit).Contains(x)).PickRandom();
                    allCookieNames[i] = teaBiscuitNames[cookieIndices[i]];
                    cookieRenders[i].material.mainTexture = teaBiscuitTextures[cookieIndices[i]];
                    break;
                case CookieCategory.chocolateButterBiscuit:
                    cookieIndices[i] = Enumerable.Range(0, 12).Where(x => !cookieIndices.Where((_, ix) => allCookieCategories[ix] == CookieCategory.chocolateButterBiscuit).Contains(x)).PickRandom();
                    allCookieNames[i] = chocolateButterBiscuitNames[cookieIndices[i]];
                    cookieRenders[i].material.mainTexture = chocolateButterBiscuitTextures[cookieIndices[i]];
                    break;
                case CookieCategory.branded:
                    // Branded cookies need to be assigned after every other cookie so that they don't point at other branded cookies or tea biscuits
                    break;
                case CookieCategory.danishButter:
                    cookieIndices[i] = Enumerable.Range(0, 5).Where(x => !cookieIndices.Where((_, ix) => allCookieCategories[ix] == CookieCategory.danishButter).Contains(x)).PickRandom();
                    allCookieNames[i] = danishButterCookieNames[cookieIndices[i]];
                    cookieRenders[i].material.mainTexture = danishButterCookieTextures[cookieIndices[i]];
                    break;
                case CookieCategory.macaron:
                    cookieIndices[i] = Enumerable.Range(0, 9).Where(x => !cookieIndices.Where((_, ix) => allCookieCategories[ix] == CookieCategory.macaron).Contains(x)).PickRandom();
                    allCookieNames[i] = macaronNames[cookieIndices[i]];
                    cookieRenders[i].material.mainTexture = macaronTextures[cookieIndices[i]];
                    break;
                case CookieCategory.notCookie:
                    cookieIndices[i] = Enumerable.Range(0, 12).Where(x => !cookieIndices.Where((_, ix) => allCookieCategories[ix] == CookieCategory.notCookie).Contains(x)).PickRandom();
                    allCookieNames[i] = notCookieNames[cookieIndices[i]];
                    cookieRenders[i].material.mainTexture = notCookieTextures[cookieIndices[i]];
                    break;
                case CookieCategory.seasonal:
                    cookieIndices[i] = Enumerable.Range(0, 21).Where(x => !cookieIndices.Where((_, ix) => allCookieCategories[ix] == CookieCategory.seasonal).Contains(x)).PickRandom();
                    allCookieNames[i] = seasonalCookieNames[cookieIndices[i]];
                    cookieRenders[i].material.mainTexture = seasonalCookieTextures[cookieIndices[i]];
                    break;
            }
        }
        for (int i = 0; i < 12; i++)
        {
            if (allCookieCategories[i] != CookieCategory.branded)
                continue;
            var potentialDirections = new List<int>();
            for (int j = 0; j < 8; j++)
                if (pointIndices[i][j] != -1 && allCookieCategories[pointIndices[i][j]] != CookieCategory.branded && allCookieCategories[pointIndices[i][j]] != CookieCategory.teaBiscuit)
                    potentialDirections.Add(j);
            try
            {
                cookieIndices[i] = Enumerable.Range(0, 16).Where(x => potentialDirections.Contains(x / 2) && !Enumerable.Range(0, 12).Where((_, ix) => allCookieCategories[ix] == CookieCategory.branded).Contains(x)).PickRandom();
            }
            catch (InvalidOperationException)
            {
                goto tryAgain;
            }
            allCookieNames[i] = brandedNames[cookieIndices[i]];
            cookieRenders[i].material.mainTexture = brandedTextures[cookieIndices[i]];
        }

        // Processing (in this order: everything that isn't branded or tea biscuit, branded, tea biscuit)
        loggingStrings.Clear();
        for (int i = 0; i < 12; i++)
        {
            if (allCookieCategories[i] == CookieCategory.teaBiscuit || allCookieCategories[i] == CookieCategory.branded) // Tea biscuits and branded cookies need to be processed after every other cookie
                continue;
            switch (allCookieCategories[i])
            {
                case CookieCategory.regular:
                    var containingSquareIx = Array.IndexOf(allSquares, allSquares.First(x => x.Contains(regularCookieNames[cookieIndices[i]])));
                    var containingSquare = allSquares.First(x => x.Contains(regularCookieNames[cookieIndices[i]]));
                    var otherRegulars = new List<int>();
                    for (int j = 0; j < 12; j++)
                        if (allCookieCategories[j] == CookieCategory.regular && j != i)
                            otherRegulars.Add(j);
                    var value1 = otherRegulars.Select(x => allCookieNames[x]).Any(x => containingSquare.Contains(x));
                    var TLorBR = Array.IndexOf(containingSquare, regularCookieNames[cookieIndices[i]]) == 0 || Array.IndexOf(containingSquare, regularCookieNames[cookieIndices[i]]) == 3;
                    var value2 = false;
                    if (TLorBR)
                    {
                        var vertOffsets = new int[] { -4, 4 };
                        for (int j = 0; j < 2; j++)
                        {
                            if ((containingSquareIx / 4 == 0 && j == 0) || ((containingSquareIx + 1) / 4 == 8 && j == 1))
                                continue;
                            Debug.Log(allSquares.Length);
                            var thisVertiSquare = allSquares[containingSquareIx + vertOffsets[j]];
                            for (int k = 0; k < 4; k++)
                                if (otherRegulars.Select(x => allCookieNames[x]).Contains(thisVertiSquare[k]))
                                    value2 = true;
                        }
                    }
                    else
                    {
                        var horiOffsets = new int[] { -1, 1 };
                        for (int j = 0; j < 2; j++)
                        {
                            if ((containingSquareIx % 4 == 0 && j == 0) || (containingSquareIx % 4 == 3 && j == 1) || (j == 1 && containingSquareIx == 34))
                                continue;
                            var thisHoriSquare = allSquares[containingSquareIx + horiOffsets[j]];
                            for (int k = 0; k < 4; k++)
                                if (otherRegulars.Select(x => allCookieNames[x]).Contains(thisHoriSquare[k]))
                                    value2 = true;
                        }
                    }
                    solution[i] = value1 ^ value2;
                    loggingStrings.Add(string.Format("{0} ({1}): {2} XOR {3} = {4}. This cookie is {5}.", coordinates[i], allCookieNames[i], value1, value2.ToString().ToLowerInvariant(), solution[i].ToString().ToLowerInvariant(), solution[i] ? "valid" : "invalid"));
                    break;
                case CookieCategory.chocolateButterBiscuit:
                    if (cookieIndices[i] == 0)
                    {
                        loggingStrings.Add(string.Format("{0} (Milk chocolate butter biscuit): The milk chocolate butter biscuit is always valid.", coordinates[i]));
                        solution[i] = true;
                    }
                    else if (cookieIndices[i] == 11)
                    {
                        solution[i] = allCookieCategories.Count(c => c == CookieCategory.chocolateButterBiscuit) == 1;
                        loggingStrings.Add(string.Format("{0} (Everybutter biscuit): This is{1} the only chocolate butter biscuit on the module, so it is {2}valid.", solution[i] ? "" : " not", solution[i] ? "" : "in"));
                    }
                    else
                    {
                        var otherChocolateButterBiscuits = new List<int>();
                        for (int j = 0; j < 12; j++)
                            if (allCookieCategories[j] == CookieCategory.chocolateButterBiscuit && j != i)
                                otherChocolateButterBiscuits.Add(j);
                        var otherValues = otherChocolateButterBiscuits.Select(x => cookieIndices[x]).ToArray();
                        solution[i] = Enumerable.Range(0, 11).Where(x => x < cookieIndices[i]).All(x => otherValues.Contains(x));
                        loggingStrings.Add(string.Format("{0} ({1}): The necessary chocolate butter biscuits are {2}, so this cookie is {3}valid.", coordinates[i], allCookieNames[i], solution[i] ? "present" : "absent", solution[i] ? "" : "in"));
                    }
                    break;
                case CookieCategory.danishButter:
                    var areaNames = new string[] { "row 1", "row 2", "row 3", "column 1 or 3", "column 2 or 4" };
                    switch (cookieIndices[i])
                    {
                        case 0:
                            solution[i] = i / 4 == 0;
                            break;
                        case 1:
                            solution[i] = i / 4 == 1;
                            break;
                        case 2:
                            solution[i] = i / 4 == 2;
                            break;
                        case 3:
                            solution[i] = i % 4 == 0 || i % 4 == 2;
                            break;
                        case 4:
                            solution[i] = i % 4 == 1 || i % 4 == 3;
                            break;
                    }
                    loggingStrings.Add(string.Format("{0} ({1}): This cookie can{2} be found in {3}, so it is {4}valid.", coordinates[i], allCookieNames[i], solution[i] ? "" : "'t", areaNames[cookieIndices[i]], solution[i] ? "" : "in"));
                    break;
                case CookieCategory.macaron:
                    var digit = bomb.GetSerialNumberNumbers().Last();
                    if (digit == 0)
                    {
                        solution[i] = true;
                        loggingStrings.Add(string.Format("{0} ({1}): The last digit of the serial number is 0, so every macaron is valid.", coordinates[i], allCookieNames[i]));
                    }
                    else
                    {
                        solution[i] = digit - 1 == cookieIndices[i];
                        loggingStrings.Add(string.Format("{0} ({1}): The last digit of the serial number is {2}, so this macaron is {3}valid.", coordinates[i], allCookieNames[i], digit, solution[i] ? "" : "in"));
                    }
                    break;
                case CookieCategory.notCookie:
                    var allNotCookieSquares = new string[3][];
                    for (int j = 0; j < 3; j++)
                        allNotCookieSquares[j] = notCookieNames.Skip(4 * j).Take(4).ToArray();
                    var containingNotSquareIx = Array.IndexOf(allNotCookieSquares, allNotCookieSquares.First(x => x.Contains(notCookieNames[cookieIndices[i]])));
                    var containingNotSquare = allNotCookieSquares[containingNotSquareIx];
                    var otherNots = new List<int>();
                    for (int j = 0; j < 12; j++)
                        if (allCookieCategories[j] == CookieCategory.notCookie && j != i)
                            otherNots.Add(j);
                    solution[i] = allNotCookieSquares.Select(arr => arr[Array.IndexOf(containingNotSquare, allCookieNames[i])]).Count(str => allCookieNames.Contains(str)) == 1;
                    loggingStrings.Add(string.Format("{0} ({1}): This menu item does{2} have a unique position within the squares, so it is {3}valid.", coordinates[i], allCookieNames[i], solution[i] ? "" : "n't", solution[i] ? "" : "in"));
                    break;
                case CookieCategory.seasonal:
                    var months = new int[] { 12, 10, 2 };
                    var monthNames = new string[] { "December", "October", "February" };
                    var elWeek = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
                    var weekday = Array.IndexOf(elWeek, DateTime.Now.DayOfWeek);
                    var relevantMonth = DateTime.Now.Month == months[cookieIndices[i] / 7];
                    if (relevantMonth)
                    {
                        solution[i] = weekday != cookieIndices[i] % 7;
                        loggingStrings.Add(string.Format("{0} ({1}): It is {2}, and the day of the week is {3}. This cookie is {4}valid.", coordinates[i], allCookieNames[i], monthNames[cookieIndices[i] / 7], DateTime.Now.DayOfWeek, solution[i] ? "" : "in"));
                    }
                    else
                    {
                        solution[i] = weekday == cookieIndices[i] % 7;
                        loggingStrings.Add(string.Format("{0} ({1}): It is not {2}, and the day of the week is {3}. This cookie is {4}valid.", coordinates[i], allCookieNames[i], monthNames[cookieIndices[i] / 7], DateTime.Now.DayOfWeek, solution[i] ? "" : "in"));
                    }
                    break;
            }
        }
        for (int i = 0; i < 12; i++)
        {
            if (allCookieCategories[i] != CookieCategory.branded)
                continue;
            var directionNames = new string[8] { "up", "up-right", "right", "down-right", "down", "down-left", "left", "up-left" };
            var direction = cookieIndices[i] / 2;
            var pointedValue = solution[pointIndices[i][direction]];
            solution[i] = cookieIndices[i] % 2 == 0 ? pointedValue : !pointedValue;
            loggingStrings.Add(string.Format("{0} ({1}): This cookie points in the direction {2} at {3} which has the value {4}. This cookie is also on the {5}, so the value is {6} and this cookie is {7}.", coordinates[i], allCookieNames[i], directionNames[direction], coordinates[pointIndices[i][direction]], pointedValue.ToString().ToLowerInvariant(), cookieIndices[i] % 2 == 0 ? "left" : "right", cookieIndices[i] % 2 == 0 ? "kept the same" : "inverted", solution[i] ? "valid" : "invalid"));
        }
        var platesToToggle = new List<int>();
        for (int i = 0; i < 12; i++)
        {
            if (allCookieCategories[i] != CookieCategory.teaBiscuit)
                continue;
            foreach (int ix in adjacentIndices[i])
                platesToToggle.Add(ix);
            var edgeworkConditions = new bool[]
            {
                bomb.GetIndicators().Any(ind => bomb.GetSerialNumberLetters().Intersect(ind).Any()),
                bomb.GetPortPlates().Any(plt => plt.Contains("Serial") && plt.Contains("Parallel")) || bomb.GetPortPlates().Any(plt => plt.Length == 0),
                bomb.GetOnIndicators().Count() == bomb.GetOffIndicators().Count(),
                (bomb.GetBatteryCount(Battery.AA) > 0 && bomb.GetBatteryCount(Battery.D) == 0) || (bomb.GetBatteryCount(Battery.D) > 0 && bomb.GetBatteryCount(Battery.AA) == 0),
                IsPrime(bomb.GetModuleNames().Count()),
                !bomb.IsPortPresent(Port.RJ45) && !bomb.IsPortPresent(Port.DVI)
            };
            solution[i] = edgeworkConditions[cookieIndices[i]];
            loggingStrings.Add(string.Format("{0} ({1}): The validities of these cookies are toggled: {2}. This cookie is {3}.", coordinates[i], allCookieNames[i], adjacentIndices[i].Select(x => coordinates[x]).Join(", "), solution[i] ? "valid" : "invalid"));
        }
        foreach (int ix in platesToToggle)
            solution[ix] = !solution[ix];
        if (!solution.Any(b => b))
            goto tryAgain;
        foreach (string str in loggingStrings)
            Debug.LogFormat("[Bakery #{0}] {1}", moduleId, str);
        Debug.LogFormat("[Bakery #{0}] Solution: {1}", moduleId, Enumerable.Range(0, 12).Where(x => solution[x]).Select(x => coordinates[x]).Join(", "));
    }

    private void PressPlate(int ix)
    {
        if (moduleSolved)
            return;
        plates[ix].AddInteractionPunch(.25f);
        audio.PlaySoundAtTransform("click", plates[ix].transform);
        selected[ix] = !selected[ix];
    }

    private void Submit()
    {
        chalkboard.AddInteractionPunch(.25f);
        audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, chalkboard.transform);
        if (moduleSolved)
            return;
        if (selected.SequenceEqual(solution))
        {
            module.HandlePass();
            audio.PlaySoundAtTransform("solve", transform);
            moduleSolved = true;
            Debug.LogFormat("[Bakery #{0}] You submitted every valid cookie. Module solved!", moduleId);
            foreach (GameObject plate in plates.Select(x => x.gameObject))
                plate.SetActive(false);
            chalkboard.gameObject.SetActive(false);
            chalkboardText.transform.parent.gameObject.SetActive(false);
            rollingPin.SetActive(true);
        }
        else
        {
            module.HandleStrike();
            Debug.LogFormat("[Bakery #{0}] You submitted an invalid configuration of cookies. Strike!", moduleId);
            for (int i = 0; i < 12; i++)
                if (selected[i] != solution[i])
                    Debug.LogFormat("[Bakery #{0}] {1} was{2} selected when it should{3} have been.", moduleId, coordinates[i], selected[i] ? "" : "n't", solution[i] ? "" : "n't");
        }
    }

    private void SetHoverText(string text)
    {
        if (text == "Sablé")
            text = "Sable";
        chalkboardText.text = text ?? "";

        // Determine the width of the text mesh
        var oldParent = chalkboardText.transform.parent;
        chalkboardText.transform.parent = null;
        chalkboardText.transform.localPosition = new Vector3(0f, 0f, 0f);
        chalkboardText.transform.localRotation = Quaternion.identity;
        chalkboardText.transform.localScale = new Vector3(1f, 1f, 1f);
        var bounds = chalkboardText.gameObject.GetComponent<Renderer>().bounds.size;

        // Make sure that it fits
        chalkboardText.transform.parent = oldParent;
        chalkboardText.transform.localPosition = new Vector3(0f, -0.51f, -0.118f);
        chalkboardText.transform.localEulerAngles = new Vector3(90f, 180f, 0f);
        chalkboardText.transform.localScale = new Vector3(-.0015f * (bounds.x > 125f ? 125f / bounds.x : 1f) * 5f, -0.01852761f, -1000f);
    }

    private enum CookieCategory
    {
        regular,
        teaBiscuit,
        chocolateButterBiscuit,
        branded,
        danishButter,
        macaron,
        notCookie,
        seasonal,
        notSet
    }

    private static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        return true;
    }

    // Twitch Plays
#pragma warning disable 414
    private readonly string TwitchHelpMessage = "!{0} cycle [Hover over every plate] | !{0} cycle <A1/B2/C3> [Hover over those plates, any amount works] !{0} press <A1/B2/C3> [Press those plates, any amount works] !{0} chalkboard [Press the chalkboard]";
#pragma warning restore 414

    private IEnumerator ProcessTwitchCommand(string input)
    {
        input = input.Trim().ToLowerInvariant();
        var inputArray = input.Split(' ').ToArray();
        if (input == "cycle")
        {
            yield return null;
            for (int i = 0; i < 12; i++)
            {
                plates[i].OnHighlight();
                yield return new WaitForSeconds(1f);
                plates[i].OnHighlightEnded();
            }
        }
        else if (input == "chalkboard")
        {
            yield return null;
            chalkboard.OnInteract();
        }
        else if (inputArray[0] == "cycle")
        {
            if (inputArray.Length == 1 || inputArray.Skip(1).Any(x => !coordinates.Contains(x.ToUpperInvariant())))
                yield break;
            yield return null;
            for (int i = 1; i < inputArray.Length; i++)
            {
                var cord = Array.IndexOf(coordinates, inputArray[i].ToUpperInvariant());
                plates[cord].OnHighlight();
                yield return new WaitForSeconds(1f);
                plates[cord].OnHighlightEnded();
            }
        }
        else if (inputArray[0] == "press")
        {
            if (inputArray.Length == 1 || inputArray.Skip(1).Any(x => !coordinates.Contains(x.ToUpperInvariant())))
                yield break;
            yield return null;
            for (int i = 1; i < inputArray.Length; i++)
            {
                var cord = Array.IndexOf(coordinates, inputArray[i].ToUpperInvariant());
                plates[cord].OnInteract();
                plateRenders[cord].material.color = selected[cord] ? purpleColor : plateColor;
                yield return new WaitForSeconds(.25f);
            }
        }
        else
            yield break;
    }

    private IEnumerator TwitchHandleForcedSolve()
    {
        for (int i = 0; i < 12; i++)
        {
            if (selected[i] != solution[i])
            {
                plates[i].OnInteract();
                plateRenders[i].material.color = selected[i] ? purpleColor : plateColor;
                yield return new WaitForSeconds(.25f);
            }
        }
        yield return null;
        chalkboard.OnInteract();
    }
}
