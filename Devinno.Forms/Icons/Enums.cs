using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Icons
{
    public enum StyleFA : int { Brands = 0, Regular = 1, Solid = 2 };
    #region IconFA
    public enum IconFA
    {
        _None,
        _500Px,
        AccessibleIcon,
        Accusoft,
        AcquisitionsIncorporated,
        Ad,
        AddressBook,
        AddressCard,
        Adjust,
        Adn,
        Adversal,
        Affiliatetheme,
        Airbnb,
        AirFreshener,
        Algolia,
        AlignCenter,
        AlignJustify,
        AlignLeft,
        AlignRight,
        Alipay,
        Allergies,
        Amazon,
        AmazonPay,
        Ambulance,
        AmericanSignLanguageInterpreting,
        Amilia,
        Anchor,
        Android,
        Angellist,
        AngleDoubleDown,
        AngleDoubleLeft,
        AngleDoubleRight,
        AngleDoubleUp,
        AngleDown,
        AngleLeft,
        AngleRight,
        AngleUp,
        Angry,
        Angrycreative,
        Angular,
        Ankh,
        Apper,
        Apple,
        AppleAlt,
        ApplePay,
        AppStore,
        AppStoreIos,
        Archive,
        Archway,
        ArrowAltCircleDown,
        ArrowAltCircleLeft,
        ArrowAltCircleRight,
        ArrowAltCircleUp,
        ArrowCircleDown,
        ArrowCircleLeft,
        ArrowCircleRight,
        ArrowCircleUp,
        ArrowDown,
        ArrowLeft,
        ArrowRight,
        ArrowsAlt,
        ArrowsAltH,
        ArrowsAltV,
        ArrowUp,
        Artstation,
        AssistiveListeningSystems,
        Asterisk,
        Asymmetrik,
        At,
        Atlas,
        Atlassian,
        Atom,
        Audible,
        AudioDescription,
        Autoprefixer,
        Avianex,
        Aviato,
        Award,
        Aws,
        Baby,
        BabyCarriage,
        Backspace,
        Backward,
        Bacon,
        Bacteria,
        Bacterium,
        Bahai,
        BalanceScale,
        BalanceScaleLeft,
        BalanceScaleRight,
        Ban,
        BandAid,
        Bandcamp,
        Barcode,
        Bars,
        BaseballBall,
        BasketballBall,
        Bath,
        BatteryEmpty,
        BatteryFull,
        BatteryHalf,
        BatteryQuarter,
        BatteryThreeQuarters,
        BattleNet,
        Bed,
        Beer,
        Behance,
        BehanceSquare,
        Bell,
        BellSlash,
        BezierCurve,
        Bible,
        Bicycle,
        Biking,
        Bimobject,
        Binoculars,
        Biohazard,
        BirthdayCake,
        Bitbucket,
        Bitcoin,
        Bity,
        Blackberry,
        BlackTie,
        Blender,
        BlenderPhone,
        Blind,
        Blog,
        Blogger,
        BloggerB,
        Bluetooth,
        BluetoothB,
        Bold,
        Bolt,
        Bomb,
        Bone,
        Bong,
        Book,
        BookDead,
        Bookmark,
        BookMedical,
        BookOpen,
        BookReader,
        Bootstrap,
        BorderAll,
        BorderNone,
        BorderStyle,
        BowlingBall,
        Box,
        Boxes,
        BoxOpen,
        BoxTissue,
        Braille,
        Brain,
        BreadSlice,
        Briefcase,
        BriefcaseMedical,
        BroadcastTower,
        Broom,
        Brush,
        Btc,
        Buffer,
        Bug,
        Building,
        Bullhorn,
        Bullseye,
        Burn,
        Buromobelexperte,
        Bus,
        BusAlt,
        BusinessTime,
        BuyNLarge,
        Buysellads,
        Calculator,
        Calendar,
        CalendarAlt,
        CalendarCheck,
        CalendarDay,
        CalendarMinus,
        CalendarPlus,
        CalendarTimes,
        CalendarWeek,
        Camera,
        CameraRetro,
        Campground,
        CanadianMapleLeaf,
        CandyCane,
        Cannabis,
        Capsules,
        Car,
        CarAlt,
        Caravan,
        CarBattery,
        CarCrash,
        CaretDown,
        CaretLeft,
        CaretRight,
        CaretSquareDown,
        CaretSquareLeft,
        CaretSquareRight,
        CaretSquareUp,
        CaretUp,
        Carrot,
        CarSide,
        CartArrowDown,
        CartPlus,
        CashRegister,
        Cat,
        CcAmazonPay,
        CcAmex,
        CcApplePay,
        CcDinersClub,
        CcDiscover,
        CcJcb,
        CcMastercard,
        CcPaypal,
        CcStripe,
        CcVisa,
        Centercode,
        Centos,
        Certificate,
        Chair,
        Chalkboard,
        ChalkboardTeacher,
        ChargingStation,
        ChartArea,
        ChartBar,
        ChartLine,
        ChartPie,
        Check,
        CheckCircle,
        CheckDouble,
        CheckSquare,
        Cheese,
        Chess,
        ChessBishop,
        ChessBoard,
        ChessKing,
        ChessKnight,
        ChessPawn,
        ChessQueen,
        ChessRook,
        ChevronCircleDown,
        ChevronCircleLeft,
        ChevronCircleRight,
        ChevronCircleUp,
        ChevronDown,
        ChevronLeft,
        ChevronRight,
        ChevronUp,
        Child,
        Chrome,
        Chromecast,
        Church,
        Circle,
        CircleNotch,
        City,
        ClinicMedical,
        Clipboard,
        ClipboardCheck,
        ClipboardList,
        Clock,
        Clone,
        ClosedCaptioning,
        Cloud,
        CloudDownloadAlt,
        Cloudflare,
        CloudMeatball,
        CloudMoon,
        CloudMoonRain,
        CloudRain,
        Cloudscale,
        CloudShowersHeavy,
        Cloudsmith,
        CloudSun,
        CloudSunRain,
        CloudUploadAlt,
        Cloudversify,
        Cocktail,
        Code,
        CodeBranch,
        Codepen,
        Codiepie,
        Coffee,
        Cog,
        Cogs,
        Coins,
        Columns,
        Comment,
        CommentAlt,
        CommentDollar,
        CommentDots,
        CommentMedical,
        Comments,
        CommentsDollar,
        CommentSlash,
        CompactDisc,
        Compass,
        Compress,
        CompressAlt,
        CompressArrowsAlt,
        ConciergeBell,
        Confluence,
        Connectdevelop,
        Contao,
        Cookie,
        CookieBite,
        Copy,
        Copyright,
        CottonBureau,
        Couch,
        Cpanel,
        CreativeCommons,
        CreativeCommonsBy,
        CreativeCommonsNc,
        CreativeCommonsNcEu,
        CreativeCommonsNcJp,
        CreativeCommonsNd,
        CreativeCommonsPd,
        CreativeCommonsPdAlt,
        CreativeCommonsRemix,
        CreativeCommonsSa,
        CreativeCommonsSampling,
        CreativeCommonsSamplingPlus,
        CreativeCommonsShare,
        CreativeCommonsZero,
        CreditCard,
        CriticalRole,
        Crop,
        CropAlt,
        Cross,
        Crosshairs,
        Crow,
        Crown,
        Crutch,
        Css3,
        Css3Alt,
        Cube,
        Cubes,
        Cut,
        Cuttlefish,
        Dailymotion,
        DAndD,
        DAndDBeyond,
        Dashcube,
        Database,
        Deaf,
        Deezer,
        Delicious,
        Democrat,
        Deploydog,
        Deskpro,
        Desktop,
        Dev,
        Deviantart,
        Dharmachakra,
        Dhl,
        Diagnoses,
        Diaspora,
        Dice,
        DiceD20,
        DiceD6,
        DiceFive,
        DiceFour,
        DiceOne,
        DiceSix,
        DiceThree,
        DiceTwo,
        Digg,
        DigitalOcean,
        DigitalTachograph,
        Directions,
        Discord,
        Discourse,
        Disease,
        Divide,
        Dizzy,
        Dna,
        Dochub,
        Docker,
        Dog,
        DollarSign,
        Dolly,
        DollyFlatbed,
        Donate,
        DoorClosed,
        DoorOpen,
        DotCircle,
        Dove,
        Download,
        Draft2digital,
        DraftingCompass,
        Dragon,
        DrawPolygon,
        Dribbble,
        DribbbleSquare,
        Dropbox,
        Drum,
        DrumSteelpan,
        DrumstickBite,
        Drupal,
        Dumbbell,
        Dumpster,
        DumpsterFire,
        Dungeon,
        Dyalog,
        Earlybirds,
        Ebay,
        Edge,
        EdgeLegacy,
        Edit,
        Egg,
        Eject,
        Elementor,
        EllipsisH,
        EllipsisV,
        Ello,
        Ember,
        Empire,
        Envelope,
        EnvelopeOpen,
        EnvelopeOpenText,
        EnvelopeSquare,
        Envira,
        Equals,
        Eraser,
        Erlang,
        Ethereum,
        Ethernet,
        Etsy,
        EuroSign,
        Evernote,
        ExchangeAlt,
        Exclamation,
        ExclamationCircle,
        ExclamationTriangle,
        Expand,
        ExpandAlt,
        ExpandArrowsAlt,
        Expeditedssl,
        ExternalLinkAlt,
        ExternalLinkSquareAlt,
        Eye,
        EyeDropper,
        EyeSlash,
        Facebook,
        FacebookF,
        FacebookMessenger,
        FacebookSquare,
        Fan,
        FantasyFlightGames,
        FastBackward,
        FastForward,
        Faucet,
        Fax,
        Feather,
        FeatherAlt,
        Fedex,
        Fedora,
        Female,
        FighterJet,
        Figma,
        File,
        FileAlt,
        FileArchive,
        FileAudio,
        FileCode,
        FileContract,
        FileCsv,
        FileDownload,
        FileExcel,
        FileExport,
        FileImage,
        FileImport,
        FileInvoice,
        FileInvoiceDollar,
        FileMedical,
        FileMedicalAlt,
        FilePdf,
        FilePowerpoint,
        FilePrescription,
        FileSignature,
        FileUpload,
        FileVideo,
        FileWord,
        Fill,
        FillDrip,
        Film,
        Filter,
        Fingerprint,
        Fire,
        FireAlt,
        FireExtinguisher,
        Firefox,
        FirefoxBrowser,
        FirstAid,
        Firstdraft,
        FirstOrder,
        FirstOrderAlt,
        Fish,
        FistRaised,
        Flag,
        FlagCheckered,
        FlagUsa,
        Flask,
        Flickr,
        Flipboard,
        Flushed,
        Fly,
        Folder,
        FolderMinus,
        FolderOpen,
        FolderPlus,
        Font,
        FontAwesome,
        FontAwesomeAlt,
        FontAwesomeFlag,
        FontAwesomeLogoFull,
        Fonticons,
        FonticonsFi,
        FootballBall,
        FortAwesome,
        FortAwesomeAlt,
        Forumbee,
        Forward,
        Foursquare,
        Freebsd,
        FreeCodeCamp,
        Frog,
        Frown,
        FrownOpen,
        Fulcrum,
        FunnelDollar,
        Futbol,
        GalacticRepublic,
        GalacticSenate,
        Gamepad,
        GasPump,
        Gavel,
        Gem,
        Genderless,
        GetPocket,
        Gg,
        GgCircle,
        Ghost,
        Gift,
        Gifts,
        Git,
        GitAlt,
        Github,
        GithubAlt,
        GithubSquare,
        Gitkraken,
        Gitlab,
        GitSquare,
        Gitter,
        GlassCheers,
        Glasses,
        GlassMartini,
        GlassMartiniAlt,
        GlassWhiskey,
        Glide,
        GlideG,
        Globe,
        GlobeAfrica,
        GlobeAmericas,
        GlobeAsia,
        GlobeEurope,
        Gofore,
        GolfBall,
        Goodreads,
        GoodreadsG,
        Google,
        GoogleDrive,
        GooglePay,
        GooglePlay,
        GooglePlus,
        GooglePlusG,
        GooglePlusSquare,
        GoogleWallet,
        Gopuram,
        GraduationCap,
        Gratipay,
        Grav,
        GreaterThan,
        GreaterThanEqual,
        Grimace,
        Grin,
        GrinAlt,
        GrinBeam,
        GrinBeamSweat,
        GrinHearts,
        GrinSquint,
        GrinSquintTears,
        GrinStars,
        GrinTears,
        GrinTongue,
        GrinTongueSquint,
        GrinTongueWink,
        GrinWink,
        Gripfire,
        GripHorizontal,
        GripLines,
        GripLinesVertical,
        GripVertical,
        Grunt,
        Guilded,
        Guitar,
        Gulp,
        HackerNews,
        HackerNewsSquare,
        Hackerrank,
        Hamburger,
        Hammer,
        Hamsa,
        HandHolding,
        HandHoldingHeart,
        HandHoldingMedical,
        HandHoldingUsd,
        HandHoldingWater,
        HandLizard,
        HandMiddleFinger,
        HandPaper,
        HandPeace,
        HandPointDown,
        HandPointer,
        HandPointLeft,
        HandPointRight,
        HandPointUp,
        HandRock,
        Hands,
        HandScissors,
        Handshake,
        HandshakeAltSlash,
        HandshakeSlash,
        HandsHelping,
        HandSparkles,
        HandSpock,
        HandsWash,
        Hanukiah,
        HardHat,
        Hashtag,
        HatCowboy,
        HatCowboySide,
        HatWizard,
        Hdd,
        Heading,
        Headphones,
        HeadphonesAlt,
        Headset,
        HeadSideCough,
        HeadSideCoughSlash,
        HeadSideMask,
        HeadSideVirus,
        Heart,
        Heartbeat,
        HeartBroken,
        Helicopter,
        Highlighter,
        Hiking,
        Hippo,
        Hips,
        HireAHelper,
        History,
        Hive,
        HockeyPuck,
        HollyBerry,
        Home,
        Hooli,
        Hornbill,
        Horse,
        HorseHead,
        Hospital,
        HospitalAlt,
        HospitalSymbol,
        HospitalUser,
        Hotdog,
        Hotel,
        Hotjar,
        HotTub,
        Hourglass,
        HourglassEnd,
        HourglassHalf,
        HourglassStart,
        HouseDamage,
        HouseUser,
        Houzz,
        Hryvnia,
        HSquare,
        Html5,
        Hubspot,
        IceCream,
        Icicles,
        Icons,
        ICursor,
        IdBadge,
        IdCard,
        IdCardAlt,
        Ideal,
        Igloo,
        Image,
        Images,
        Imdb,
        Inbox,
        Indent,
        Industry,
        Infinity,
        Info,
        InfoCircle,
        Innosoft,
        Instagram,
        InstagramSquare,
        Instalod,
        Intercom,
        InternetExplorer,
        Invision,
        Ioxhost,
        Italic,
        ItchIo,
        Itunes,
        ItunesNote,
        Java,
        Jedi,
        JediOrder,
        Jenkins,
        Jira,
        Joget,
        Joint,
        Joomla,
        JournalWhills,
        Js,
        Jsfiddle,
        JsSquare,
        Kaaba,
        Kaggle,
        Key,
        Keybase,
        Keyboard,
        Keycdn,
        Khanda,
        Kickstarter,
        KickstarterK,
        Kiss,
        KissBeam,
        KissWinkHeart,
        KiwiBird,
        Korvue,
        Landmark,
        Language,
        Laptop,
        LaptopCode,
        LaptopHouse,
        LaptopMedical,
        Laravel,
        Lastfm,
        LastfmSquare,
        Laugh,
        LaughBeam,
        LaughSquint,
        LaughWink,
        LayerGroup,
        Leaf,
        Leanpub,
        Lemon,
        Less,
        LessThan,
        LessThanEqual,
        LevelDownAlt,
        LevelUpAlt,
        LifeRing,
        Lightbulb,
        Line,
        Link,
        Linkedin,
        LinkedinIn,
        Linode,
        Linux,
        LiraSign,
        List,
        ListAlt,
        ListOl,
        ListUl,
        LocationArrow,
        Lock,
        LockOpen,
        LongArrowAltDown,
        LongArrowAltLeft,
        LongArrowAltRight,
        LongArrowAltUp,
        LowVision,
        LuggageCart,
        Lungs,
        LungsVirus,
        Lyft,
        Magento,
        Magic,
        Magnet,
        MailBulk,
        Mailchimp,
        Male,
        Mandalorian,
        Map,
        MapMarked,
        MapMarkedAlt,
        MapMarker,
        MapMarkerAlt,
        MapPin,
        MapSigns,
        Markdown,
        Marker,
        Mars,
        MarsDouble,
        MarsStroke,
        MarsStrokeH,
        MarsStrokeV,
        Mask,
        Mastodon,
        Maxcdn,
        Mdb,
        Medal,
        Medapps,
        Medium,
        MediumM,
        Medkit,
        Medrt,
        Meetup,
        Megaport,
        Meh,
        MehBlank,
        MehRollingEyes,
        Memory,
        Mendeley,
        Menorah,
        Mercury,
        Meteor,
        Microblog,
        Microchip,
        Microphone,
        MicrophoneAlt,
        MicrophoneAltSlash,
        MicrophoneSlash,
        Microscope,
        Microsoft,
        Minus,
        MinusCircle,
        MinusSquare,
        Mitten,
        Mix,
        Mixcloud,
        Mixer,
        Mizuni,
        Mobile,
        MobileAlt,
        Modx,
        Monero,
        MoneyBill,
        MoneyBillAlt,
        MoneyBillWave,
        MoneyBillWaveAlt,
        MoneyCheck,
        MoneyCheckAlt,
        Monument,
        Moon,
        MortarPestle,
        Mosque,
        Motorcycle,
        Mountain,
        Mouse,
        MousePointer,
        MugHot,
        Music,
        Napster,
        Neos,
        NetworkWired,
        Neuter,
        Newspaper,
        Nimblr,
        Node,
        NodeJs,
        NotEqual,
        NotesMedical,
        Npm,
        Ns8,
        Nutritionix,
        ObjectGroup,
        ObjectUngroup,
        OctopusDeploy,
        Odnoklassniki,
        OdnoklassnikiSquare,
        OilCan,
        OldRepublic,
        Om,
        Opencart,
        Openid,
        Opera,
        OptinMonster,
        Orcid,
        Osi,
        Otter,
        Outdent,
        Page4,
        Pagelines,
        Pager,
        PaintBrush,
        PaintRoller,
        Palette,
        Palfed,
        Pallet,
        Paperclip,
        PaperPlane,
        ParachuteBox,
        Paragraph,
        Parking,
        Passport,
        Pastafarianism,
        Paste,
        Patreon,
        Pause,
        PauseCircle,
        Paw,
        Paypal,
        Peace,
        Pen,
        PenAlt,
        PencilAlt,
        PencilRuler,
        PenFancy,
        PenNib,
        PennyArcade,
        PenSquare,
        PeopleArrows,
        PeopleCarry,
        PepperHot,
        Perbyte,
        Percent,
        Percentage,
        Periscope,
        PersonBooth,
        Phabricator,
        PhoenixFramework,
        PhoenixSquadron,
        Phone,
        PhoneAlt,
        PhoneSlash,
        PhoneSquare,
        PhoneSquareAlt,
        PhoneVolume,
        PhotoVideo,
        Php,
        PiedPiper,
        PiedPiperAlt,
        PiedPiperHat,
        PiedPiperPp,
        PiedPiperSquare,
        PiggyBank,
        Pills,
        Pinterest,
        PinterestP,
        PinterestSquare,
        PizzaSlice,
        PlaceOfWorship,
        Plane,
        PlaneArrival,
        PlaneDeparture,
        PlaneSlash,
        Play,
        PlayCircle,
        Playstation,
        Plug,
        Plus,
        PlusCircle,
        PlusSquare,
        Podcast,
        Poll,
        PollH,
        Poo,
        Poop,
        PooStorm,
        Portrait,
        PoundSign,
        PowerOff,
        Pray,
        PrayingHands,
        Prescription,
        PrescriptionBottle,
        PrescriptionBottleAlt,
        Print,
        Procedures,
        ProductHunt,
        ProjectDiagram,
        PumpMedical,
        PumpSoap,
        Pushed,
        PuzzlePiece,
        Python,
        Qq,
        Qrcode,
        Question,
        QuestionCircle,
        Quidditch,
        Quinscape,
        Quora,
        QuoteLeft,
        QuoteRight,
        Quran,
        Radiation,
        RadiationAlt,
        Rainbow,
        Random,
        RaspberryPi,
        Ravelry,
        React,
        Reacteurope,
        Readme,
        Rebel,
        Receipt,
        RecordVinyl,
        Recycle,
        Reddit,
        RedditAlien,
        RedditSquare,
        Redhat,
        Redo,
        RedoAlt,
        RedRiver,
        Registered,
        RemoveFormat,
        Renren,
        Reply,
        ReplyAll,
        Replyd,
        Republican,
        Researchgate,
        Resolving,
        Restroom,
        Retweet,
        Rev,
        Ribbon,
        Ring,
        Road,
        Robot,
        Rocket,
        Rocketchat,
        Rockrms,
        Route,
        RProject,
        Rss,
        RssSquare,
        RubleSign,
        Ruler,
        RulerCombined,
        RulerHorizontal,
        RulerVertical,
        Running,
        RupeeSign,
        Rust,
        SadCry,
        SadTear,
        Safari,
        Salesforce,
        Sass,
        Satellite,
        SatelliteDish,
        Save,
        Schlix,
        School,
        Screwdriver,
        Scribd,
        Scroll,
        SdCard,
        Search,
        SearchDollar,
        Searchengin,
        SearchLocation,
        SearchMinus,
        SearchPlus,
        Seedling,
        Sellcast,
        Sellsy,
        Server,
        Servicestack,
        Shapes,
        Share,
        ShareAlt,
        ShareAltSquare,
        ShareSquare,
        ShekelSign,
        ShieldAlt,
        ShieldVirus,
        Ship,
        ShippingFast,
        Shirtsinbulk,
        ShoePrints,
        Shopify,
        ShoppingBag,
        ShoppingBasket,
        ShoppingCart,
        Shopware,
        Shower,
        ShuttleVan,
        Sign,
        Signal,
        Signature,
        SignInAlt,
        SignLanguage,
        SignOutAlt,
        SimCard,
        Simplybuilt,
        Sink,
        Sistrix,
        Sitemap,
        Sith,
        Skating,
        Sketch,
        Skiing,
        SkiingNordic,
        Skull,
        SkullCrossbones,
        Skyatlas,
        Skype,
        Slack,
        SlackHash,
        Slash,
        Sleigh,
        SlidersH,
        Slideshare,
        Smile,
        SmileBeam,
        SmileWink,
        Smog,
        Smoking,
        SmokingBan,
        Sms,
        Snapchat,
        SnapchatGhost,
        SnapchatSquare,
        Snowboarding,
        Snowflake,
        Snowman,
        Snowplow,
        Soap,
        Socks,
        SolarPanel,
        Sort,
        SortAlphaDown,
        SortAlphaDownAlt,
        SortAlphaUp,
        SortAlphaUpAlt,
        SortAmountDown,
        SortAmountDownAlt,
        SortAmountUp,
        SortAmountUpAlt,
        SortDown,
        SortNumericDown,
        SortNumericDownAlt,
        SortNumericUp,
        SortNumericUpAlt,
        SortUp,
        Soundcloud,
        Sourcetree,
        Spa,
        SpaceShuttle,
        Speakap,
        SpeakerDeck,
        SpellCheck,
        Spider,
        Spinner,
        Splotch,
        Spotify,
        SprayCan,
        Square,
        SquareFull,
        SquareRootAlt,
        Squarespace,
        StackExchange,
        StackOverflow,
        Stackpath,
        Stamp,
        Star,
        StarAndCrescent,
        StarHalf,
        StarHalfAlt,
        StarOfDavid,
        StarOfLife,
        Staylinked,
        Steam,
        SteamSquare,
        SteamSymbol,
        StepBackward,
        StepForward,
        Stethoscope,
        StickerMule,
        StickyNote,
        Stop,
        StopCircle,
        Stopwatch,
        Stopwatch20,
        Store,
        StoreAlt,
        StoreAltSlash,
        StoreSlash,
        Strava,
        Stream,
        StreetView,
        Strikethrough,
        Stripe,
        StripeS,
        Stroopwafel,
        Studiovinari,
        Stumbleupon,
        StumbleuponCircle,
        Subscript,
        Subway,
        Suitcase,
        SuitcaseRolling,
        Sun,
        Superpowers,
        Superscript,
        Supple,
        Surprise,
        Suse,
        Swatchbook,
        Swift,
        Swimmer,
        SwimmingPool,
        Symfony,
        Synagogue,
        Sync,
        SyncAlt,
        Syringe,
        Table,
        Tablet,
        TabletAlt,
        TableTennis,
        Tablets,
        TachometerAlt,
        Tag,
        Tags,
        Tape,
        Tasks,
        Taxi,
        Teamspeak,
        Teeth,
        TeethOpen,
        Telegram,
        TelegramPlane,
        TemperatureHigh,
        TemperatureLow,
        TencentWeibo,
        Tenge,
        Terminal,
        TextHeight,
        TextWidth,
        Th,
        TheaterMasks,
        Themeco,
        Themeisle,
        TheRedYeti,
        Thermometer,
        ThermometerEmpty,
        ThermometerFull,
        ThermometerHalf,
        ThermometerQuarter,
        ThermometerThreeQuarters,
        ThinkPeaks,
        ThLarge,
        ThList,
        ThumbsDown,
        ThumbsUp,
        Thumbtack,
        TicketAlt,
        Tiktok,
        Times,
        TimesCircle,
        Tint,
        TintSlash,
        Tired,
        ToggleOff,
        ToggleOn,
        Toilet,
        ToiletPaper,
        ToiletPaperSlash,
        Toolbox,
        Tools,
        Tooth,
        Torah,
        ToriiGate,
        Tractor,
        TradeFederation,
        Trademark,
        TrafficLight,
        Trailer,
        Train,
        Tram,
        Transgender,
        TransgenderAlt,
        Trash,
        TrashAlt,
        TrashRestore,
        TrashRestoreAlt,
        Tree,
        Trello,
        Tripadvisor,
        Trophy,
        Truck,
        TruckLoading,
        TruckMonster,
        TruckMoving,
        TruckPickup,
        Tshirt,
        Tty,
        Tumblr,
        TumblrSquare,
        Tv,
        Twitch,
        Twitter,
        TwitterSquare,
        Typo3,
        Uber,
        Ubuntu,
        Uikit,
        Umbraco,
        Umbrella,
        UmbrellaBeach,
        Uncharted,
        Underline,
        Undo,
        UndoAlt,
        Uniregistry,
        Unity,
        UniversalAccess,
        University,
        Unlink,
        Unlock,
        UnlockAlt,
        Unsplash,
        Untappd,
        Upload,
        Ups,
        Usb,
        User,
        UserAlt,
        UserAltSlash,
        UserAstronaut,
        UserCheck,
        UserCircle,
        UserClock,
        UserCog,
        UserEdit,
        UserFriends,
        UserGraduate,
        UserInjured,
        UserLock,
        UserMd,
        UserMinus,
        UserNinja,
        UserNurse,
        UserPlus,
        Users,
        UsersCog,
        UserSecret,
        UserShield,
        UserSlash,
        UsersSlash,
        UserTag,
        UserTie,
        UserTimes,
        Usps,
        Ussunnah,
        Utensils,
        UtensilSpoon,
        Vaadin,
        VectorSquare,
        Venus,
        VenusDouble,
        VenusMars,
        Vest,
        VestPatches,
        Viacoin,
        Viadeo,
        ViadeoSquare,
        Vial,
        Vials,
        Viber,
        Video,
        VideoSlash,
        Vihara,
        Vimeo,
        VimeoSquare,
        VimeoV,
        Vine,
        Virus,
        Viruses,
        VirusSlash,
        Vk,
        Vnv,
        Voicemail,
        VolleyballBall,
        VolumeDown,
        VolumeMute,
        VolumeOff,
        VolumeUp,
        VoteYea,
        VrCardboard,
        Vuejs,
        Walking,
        Wallet,
        Warehouse,
        WatchmanMonitoring,
        Water,
        WaveSquare,
        Waze,
        Weebly,
        Weibo,
        Weight,
        WeightHanging,
        Weixin,
        Whatsapp,
        WhatsappSquare,
        Wheelchair,
        Whmcs,
        Wifi,
        WikipediaW,
        Wind,
        WindowClose,
        WindowMaximize,
        WindowMinimize,
        WindowRestore,
        Windows,
        WineBottle,
        WineGlass,
        WineGlassAlt,
        Wix,
        WizardsOfTheCoast,
        Wodu,
        WolfPackBattalion,
        WonSign,
        Wordpress,
        WordpressSimple,
        Wpbeginner,
        Wpexplorer,
        Wpforms,
        Wpressr,
        Wrench,
        Xbox,
        Xing,
        XingSquare,
        XRay,
        Yahoo,
        Yammer,
        Yandex,
        YandexInternational,
        Yarn,
        YCombinator,
        Yelp,
        YenSign,
        YinYang,
        Yoast,
        Youtube,
        YoutubeSquare,
        Zhihu,
    }
    #endregion
    #region Remark
    /*
    public enum IconFA
    {
        _None = 0,
        _500Px = 0xf26e,
        AccessibleIcon = 0xf368,
        Accusoft = 0xf369,
        AcquisitionsIncorporated = 0xf6af,
        Ad = 0xf641,
        AddressBook = 0xf2b9,
        AddressCard = 0xf2bb,
        Adjust = 0xf042,
        Adn = 0xf170,
        Adversal = 0xf36a,
        Affiliatetheme = 0xf36b,
        Airbnb = 0xf834,
        AirFreshener = 0xf5d0,
        Algolia = 0xf36c,
        AlignCenter = 0xf037,
        AlignJustify = 0xf039,
        AlignLeft = 0xf036,
        AlignRight = 0xf038,
        Alipay = 0xf642,
        Allergies = 0xf461,
        Amazon = 0xf270,
        AmazonPay = 0xf42c,
        Ambulance = 0xf0f9,
        AmericanSignLanguageInterpreting = 0xf2a3,
        Amilia = 0xf36d,
        Anchor = 0xf13d,
        Android = 0xf17b,
        Angellist = 0xf209,
        AngleDoubleDown = 0xf103,
        AngleDoubleLeft = 0xf100,
        AngleDoubleRight = 0xf101,
        AngleDoubleUp = 0xf102,
        AngleDown = 0xf107,
        AngleLeft = 0xf104,
        AngleRight = 0xf105,
        AngleUp = 0xf106,
        Angry = 0xf556,
        Angrycreative = 0xf36e,
        Angular = 0xf420,
        Ankh = 0xf644,
        Apper = 0xf371,
        Apple = 0xf179,
        AppleAlt = 0xf5d1,
        ApplePay = 0xf415,
        AppStore = 0xf36f,
        AppStoreIos = 0xf370,
        Archive = 0xf187,
        Archway = 0xf557,
        ArrowAltCircleDown = 0xf358,
        ArrowAltCircleLeft = 0xf359,
        ArrowAltCircleRight = 0xf35a,
        ArrowAltCircleUp = 0xf35b,
        ArrowCircleDown = 0xf0ab,
        ArrowCircleLeft = 0xf0a8,
        ArrowCircleRight = 0xf0a9,
        ArrowCircleUp = 0xf0aa,
        ArrowDown = 0xf063,
        ArrowLeft = 0xf060,
        ArrowRight = 0xf061,
        ArrowsAlt = 0xf0b2,
        ArrowsAltH = 0xf337,
        ArrowsAltV = 0xf338,
        ArrowUp = 0xf062,
        Artstation = 0xf77a,
        AssistiveListeningSystems = 0xf2a2,
        Asterisk = 0xf069,
        Asymmetrik = 0xf372,
        At = 0xf1fa,
        Atlas = 0xf558,
        Atlassian = 0xf77b,
        Atom = 0xf5d2,
        Audible = 0xf373,
        AudioDescription = 0xf29e,
        Autoprefixer = 0xf41c,
        Avianex = 0xf374,
        Aviato = 0xf421,
        Award = 0xf559,
        Aws = 0xf375,
        Baby = 0xf77c,
        BabyCarriage = 0xf77d,
        Backspace = 0xf55a,
        Backward = 0xf04a,
        Bacon = 0xf7e5,
        Bacteria = 0xe059,
        Bacterium = 0xe05a,
        Bahai = 0xf666,
        BalanceScale = 0xf24e,
        BalanceScaleLeft = 0xf515,
        BalanceScaleRight = 0xf516,
        Ban = 0xf05e,
        BandAid = 0xf462,
        Bandcamp = 0xf2d5,
        Barcode = 0xf02a,
        Bars = 0xf0c9,
        BaseballBall = 0xf433,
        BasketballBall = 0xf434,
        Bath = 0xf2cd,
        BatteryEmpty = 0xf244,
        BatteryFull = 0xf240,
        BatteryHalf = 0xf242,
        BatteryQuarter = 0xf243,
        BatteryThreeQuarters = 0xf241,
        BattleNet = 0xf835,
        Bed = 0xf236,
        Beer = 0xf0fc,
        Behance = 0xf1b4,
        BehanceSquare = 0xf1b5,
        Bell = 0xf0f3,
        BellSlash = 0xf1f6,
        BezierCurve = 0xf55b,
        Bible = 0xf647,
        Bicycle = 0xf206,
        Biking = 0xf84a,
        Bimobject = 0xf378,
        Binoculars = 0xf1e5,
        Biohazard = 0xf780,
        BirthdayCake = 0xf1fd,
        Bitbucket = 0xf171,
        Bitcoin = 0xf379,
        Bity = 0xf37a,
        Blackberry = 0xf37b,
        BlackTie = 0xf27e,
        Blender = 0xf517,
        BlenderPhone = 0xf6b6,
        Blind = 0xf29d,
        Blog = 0xf781,
        Blogger = 0xf37c,
        BloggerB = 0xf37d,
        Bluetooth = 0xf293,
        BluetoothB = 0xf294,
        Bold = 0xf032,
        Bolt = 0xf0e7,
        Bomb = 0xf1e2,
        Bone = 0xf5d7,
        Bong = 0xf55c,
        Book = 0xf02d,
        BookDead = 0xf6b7,
        Bookmark = 0xf02e,
        BookMedical = 0xf7e6,
        BookOpen = 0xf518,
        BookReader = 0xf5da,
        Bootstrap = 0xf836,
        BorderAll = 0xf84c,
        BorderNone = 0xf850,
        BorderStyle = 0xf853,
        BowlingBall = 0xf436,
        Box = 0xf466,
        Boxes = 0xf468,
        BoxOpen = 0xf49e,
        BoxTissue = 0xe05b,
        Braille = 0xf2a1,
        Brain = 0xf5dc,
        BreadSlice = 0xf7ec,
        Briefcase = 0xf0b1,
        BriefcaseMedical = 0xf469,
        BroadcastTower = 0xf519,
        Broom = 0xf51a,
        Brush = 0xf55d,
        Btc = 0xf15a,
        Buffer = 0xf837,
        Bug = 0xf188,
        Building = 0xf1ad,
        Bullhorn = 0xf0a1,
        Bullseye = 0xf140,
        Burn = 0xf46a,
        Buromobelexperte = 0xf37f,
        Bus = 0xf207,
        BusAlt = 0xf55e,
        BusinessTime = 0xf64a,
        BuyNLarge = 0xf8a6,
        Buysellads = 0xf20d,
        Calculator = 0xf1ec,
        Calendar = 0xf133,
        CalendarAlt = 0xf073,
        CalendarCheck = 0xf274,
        CalendarDay = 0xf783,
        CalendarMinus = 0xf272,
        CalendarPlus = 0xf271,
        CalendarTimes = 0xf273,
        CalendarWeek = 0xf784,
        Camera = 0xf030,
        CameraRetro = 0xf083,
        Campground = 0xf6bb,
        CanadianMapleLeaf = 0xf785,
        CandyCane = 0xf786,
        Cannabis = 0xf55f,
        Capsules = 0xf46b,
        Car = 0xf1b9,
        CarAlt = 0xf5de,
        Caravan = 0xf8ff,
        CarBattery = 0xf5df,
        CarCrash = 0xf5e1,
        CaretDown = 0xf0d7,
        CaretLeft = 0xf0d9,
        CaretRight = 0xf0da,
        CaretSquareDown = 0xf150,
        CaretSquareLeft = 0xf191,
        CaretSquareRight = 0xf152,
        CaretSquareUp = 0xf151,
        CaretUp = 0xf0d8,
        Carrot = 0xf787,
        CarSide = 0xf5e4,
        CartArrowDown = 0xf218,
        CartPlus = 0xf217,
        CashRegister = 0xf788,
        Cat = 0xf6be,
        CcAmazonPay = 0xf42d,
        CcAmex = 0xf1f3,
        CcApplePay = 0xf416,
        CcDinersClub = 0xf24c,
        CcDiscover = 0xf1f2,
        CcJcb = 0xf24b,
        CcMastercard = 0xf1f1,
        CcPaypal = 0xf1f4,
        CcStripe = 0xf1f5,
        CcVisa = 0xf1f0,
        Centercode = 0xf380,
        Centos = 0xf789,
        Certificate = 0xf0a3,
        Chair = 0xf6c0,
        Chalkboard = 0xf51b,
        ChalkboardTeacher = 0xf51c,
        ChargingStation = 0xf5e7,
        ChartArea = 0xf1fe,
        ChartBar = 0xf080,
        ChartLine = 0xf201,
        ChartPie = 0xf200,
        Check = 0xf00c,
        CheckCircle = 0xf058,
        CheckDouble = 0xf560,
        CheckSquare = 0xf14a,
        Cheese = 0xf7ef,
        Chess = 0xf439,
        ChessBishop = 0xf43a,
        ChessBoard = 0xf43c,
        ChessKing = 0xf43f,
        ChessKnight = 0xf441,
        ChessPawn = 0xf443,
        ChessQueen = 0xf445,
        ChessRook = 0xf447,
        ChevronCircleDown = 0xf13a,
        ChevronCircleLeft = 0xf137,
        ChevronCircleRight = 0xf138,
        ChevronCircleUp = 0xf139,
        ChevronDown = 0xf078,
        ChevronLeft = 0xf053,
        ChevronRight = 0xf054,
        ChevronUp = 0xf077,
        Child = 0xf1ae,
        Chrome = 0xf268,
        Chromecast = 0xf838,
        Church = 0xf51d,
        Circle = 0xf111,
        CircleNotch = 0xf1ce,
        City = 0xf64f,
        ClinicMedical = 0xf7f2,
        Clipboard = 0xf328,
        ClipboardCheck = 0xf46c,
        ClipboardList = 0xf46d,
        Clock = 0xf017,
        Clone = 0xf24d,
        ClosedCaptioning = 0xf20a,
        Cloud = 0xf0c2,
        CloudDownloadAlt = 0xf381,
        Cloudflare = 0xe07d,
        CloudMeatball = 0xf73b,
        CloudMoon = 0xf6c3,
        CloudMoonRain = 0xf73c,
        CloudRain = 0xf73d,
        Cloudscale = 0xf383,
        CloudShowersHeavy = 0xf740,
        Cloudsmith = 0xf384,
        CloudSun = 0xf6c4,
        CloudSunRain = 0xf743,
        CloudUploadAlt = 0xf382,
        Cloudversify = 0xf385,
        Cocktail = 0xf561,
        Code = 0xf121,
        CodeBranch = 0xf126,
        Codepen = 0xf1cb,
        Codiepie = 0xf284,
        Coffee = 0xf0f4,
        Cog = 0xf013,
        Cogs = 0xf085,
        Coins = 0xf51e,
        Columns = 0xf0db,
        Comment = 0xf075,
        CommentAlt = 0xf27a,
        CommentDollar = 0xf651,
        CommentDots = 0xf4ad,
        CommentMedical = 0xf7f5,
        Comments = 0xf086,
        CommentsDollar = 0xf653,
        CommentSlash = 0xf4b3,
        CompactDisc = 0xf51f,
        Compass = 0xf14e,
        Compress = 0xf066,
        CompressAlt = 0xf422,
        CompressArrowsAlt = 0xf78c,
        ConciergeBell = 0xf562,
        Confluence = 0xf78d,
        Connectdevelop = 0xf20e,
        Contao = 0xf26d,
        Cookie = 0xf563,
        CookieBite = 0xf564,
        Copy = 0xf0c5,
        Copyright = 0xf1f9,
        CottonBureau = 0xf89e,
        Couch = 0xf4b8,
        Cpanel = 0xf388,
        CreativeCommons = 0xf25e,
        CreativeCommonsBy = 0xf4e7,
        CreativeCommonsNc = 0xf4e8,
        CreativeCommonsNcEu = 0xf4e9,
        CreativeCommonsNcJp = 0xf4ea,
        CreativeCommonsNd = 0xf4eb,
        CreativeCommonsPd = 0xf4ec,
        CreativeCommonsPdAlt = 0xf4ed,
        CreativeCommonsRemix = 0xf4ee,
        CreativeCommonsSa = 0xf4ef,
        CreativeCommonsSampling = 0xf4f0,
        CreativeCommonsSamplingPlus = 0xf4f1,
        CreativeCommonsShare = 0xf4f2,
        CreativeCommonsZero = 0xf4f3,
        CreditCard = 0xf09d,
        CriticalRole = 0xf6c9,
        Crop = 0xf125,
        CropAlt = 0xf565,
        Cross = 0xf654,
        Crosshairs = 0xf05b,
        Crow = 0xf520,
        Crown = 0xf521,
        Crutch = 0xf7f7,
        Css3 = 0xf13c,
        Css3Alt = 0xf38b,
        Cube = 0xf1b2,
        Cubes = 0xf1b3,
        Cut = 0xf0c4,
        Cuttlefish = 0xf38c,
        Dailymotion = 0xe052,
        DAndD = 0xf38d,
        DAndDBeyond = 0xf6ca,
        Dashcube = 0xf210,
        Database = 0xf1c0,
        Deaf = 0xf2a4,
        Deezer = 0xe077,
        Delicious = 0xf1a5,
        Democrat = 0xf747,
        Deploydog = 0xf38e,
        Deskpro = 0xf38f,
        Desktop = 0xf108,
        Dev = 0xf6cc,
        Deviantart = 0xf1bd,
        Dharmachakra = 0xf655,
        Dhl = 0xf790,
        Diagnoses = 0xf470,
        Diaspora = 0xf791,
        Dice = 0xf522,
        DiceD20 = 0xf6cf,
        DiceD6 = 0xf6d1,
        DiceFive = 0xf523,
        DiceFour = 0xf524,
        DiceOne = 0xf525,
        DiceSix = 0xf526,
        DiceThree = 0xf527,
        DiceTwo = 0xf528,
        Digg = 0xf1a6,
        DigitalOcean = 0xf391,
        DigitalTachograph = 0xf566,
        Directions = 0xf5eb,
        Discord = 0xf392,
        Discourse = 0xf393,
        Disease = 0xf7fa,
        Divide = 0xf529,
        Dizzy = 0xf567,
        Dna = 0xf471,
        Dochub = 0xf394,
        Docker = 0xf395,
        Dog = 0xf6d3,
        DollarSign = 0xf155,
        Dolly = 0xf472,
        DollyFlatbed = 0xf474,
        Donate = 0xf4b9,
        DoorClosed = 0xf52a,
        DoorOpen = 0xf52b,
        DotCircle = 0xf192,
        Dove = 0xf4ba,
        Download = 0xf019,
        Draft2digital = 0xf396,
        DraftingCompass = 0xf568,
        Dragon = 0xf6d5,
        DrawPolygon = 0xf5ee,
        Dribbble = 0xf17d,
        DribbbleSquare = 0xf397,
        Dropbox = 0xf16b,
        Drum = 0xf569,
        DrumSteelpan = 0xf56a,
        DrumstickBite = 0xf6d7,
        Drupal = 0xf1a9,
        Dumbbell = 0xf44b,
        Dumpster = 0xf793,
        DumpsterFire = 0xf794,
        Dungeon = 0xf6d9,
        Dyalog = 0xf399,
        Earlybirds = 0xf39a,
        Ebay = 0xf4f4,
        Edge = 0xf282,
        EdgeLegacy = 0xe078,
        Edit = 0xf044,
        Egg = 0xf7fb,
        Eject = 0xf052,
        Elementor = 0xf430,
        EllipsisH = 0xf141,
        EllipsisV = 0xf142,
        Ello = 0xf5f1,
        Ember = 0xf423,
        Empire = 0xf1d1,
        Envelope = 0xf0e0,
        EnvelopeOpen = 0xf2b6,
        EnvelopeOpenText = 0xf658,
        EnvelopeSquare = 0xf199,
        Envira = 0xf299,
        Equals = 0xf52c,
        Eraser = 0xf12d,
        Erlang = 0xf39d,
        Ethereum = 0xf42e,
        Ethernet = 0xf796,
        Etsy = 0xf2d7,
        EuroSign = 0xf153,
        Evernote = 0xf839,
        ExchangeAlt = 0xf362,
        Exclamation = 0xf12a,
        ExclamationCircle = 0xf06a,
        ExclamationTriangle = 0xf071,
        Expand = 0xf065,
        ExpandAlt = 0xf424,
        ExpandArrowsAlt = 0xf31e,
        Expeditedssl = 0xf23e,
        ExternalLinkAlt = 0xf35d,
        ExternalLinkSquareAlt = 0xf360,
        Eye = 0xf06e,
        EyeDropper = 0xf1fb,
        EyeSlash = 0xf070,
        Facebook = 0xf09a,
        FacebookF = 0xf39e,
        FacebookMessenger = 0xf39f,
        FacebookSquare = 0xf082,
        Fan = 0xf863,
        FantasyFlightGames = 0xf6dc,
        FastBackward = 0xf049,
        FastForward = 0xf050,
        Faucet = 0xe005,
        Fax = 0xf1ac,
        Feather = 0xf52d,
        FeatherAlt = 0xf56b,
        Fedex = 0xf797,
        Fedora = 0xf798,
        Female = 0xf182,
        FighterJet = 0xf0fb,
        Figma = 0xf799,
        File = 0xf15b,
        FileAlt = 0xf15c,
        FileArchive = 0xf1c6,
        FileAudio = 0xf1c7,
        FileCode = 0xf1c9,
        FileContract = 0xf56c,
        FileCsv = 0xf6dd,
        FileDownload = 0xf56d,
        FileExcel = 0xf1c3,
        FileExport = 0xf56e,
        FileImage = 0xf1c5,
        FileImport = 0xf56f,
        FileInvoice = 0xf570,
        FileInvoiceDollar = 0xf571,
        FileMedical = 0xf477,
        FileMedicalAlt = 0xf478,
        FilePdf = 0xf1c1,
        FilePowerpoint = 0xf1c4,
        FilePrescription = 0xf572,
        FileSignature = 0xf573,
        FileUpload = 0xf574,
        FileVideo = 0xf1c8,
        FileWord = 0xf1c2,
        Fill = 0xf575,
        FillDrip = 0xf576,
        Film = 0xf008,
        Filter = 0xf0b0,
        Fingerprint = 0xf577,
        Fire = 0xf06d,
        FireAlt = 0xf7e4,
        FireExtinguisher = 0xf134,
        Firefox = 0xf269,
        FirefoxBrowser = 0xe007,
        FirstAid = 0xf479,
        Firstdraft = 0xf3a1,
        FirstOrder = 0xf2b0,
        FirstOrderAlt = 0xf50a,
        Fish = 0xf578,
        FistRaised = 0xf6de,
        Flag = 0xf024,
        FlagCheckered = 0xf11e,
        FlagUsa = 0xf74d,
        Flask = 0xf0c3,
        Flickr = 0xf16e,
        Flipboard = 0xf44d,
        Flushed = 0xf579,
        Fly = 0xf417,
        Folder = 0xf07b,
        FolderMinus = 0xf65d,
        FolderOpen = 0xf07c,
        FolderPlus = 0xf65e,
        Font = 0xf031,
        FontAwesome = 0xf2b4,
        FontAwesomeAlt = 0xf35c,
        FontAwesomeFlag = 0xf425,
        FontAwesomeLogoFull = 0xf4e6,
        Fonticons = 0xf280,
        FonticonsFi = 0xf3a2,
        FootballBall = 0xf44e,
        FortAwesome = 0xf286,
        FortAwesomeAlt = 0xf3a3,
        Forumbee = 0xf211,
        Forward = 0xf04e,
        Foursquare = 0xf180,
        Freebsd = 0xf3a4,
        FreeCodeCamp = 0xf2c5,
        Frog = 0xf52e,
        Frown = 0xf119,
        FrownOpen = 0xf57a,
        Fulcrum = 0xf50b,
        FunnelDollar = 0xf662,
        Futbol = 0xf1e3,
        GalacticRepublic = 0xf50c,
        GalacticSenate = 0xf50d,
        Gamepad = 0xf11b,
        GasPump = 0xf52f,
        Gavel = 0xf0e3,
        Gem = 0xf3a5,
        Genderless = 0xf22d,
        GetPocket = 0xf265,
        Gg = 0xf260,
        GgCircle = 0xf261,
        Ghost = 0xf6e2,
        Gift = 0xf06b,
        Gifts = 0xf79c,
        Git = 0xf1d3,
        GitAlt = 0xf841,
        Github = 0xf09b,
        GithubAlt = 0xf113,
        GithubSquare = 0xf092,
        Gitkraken = 0xf3a6,
        Gitlab = 0xf296,
        GitSquare = 0xf1d2,
        Gitter = 0xf426,
        GlassCheers = 0xf79f,
        Glasses = 0xf530,
        GlassMartini = 0xf000,
        GlassMartiniAlt = 0xf57b,
        GlassWhiskey = 0xf7a0,
        Glide = 0xf2a5,
        GlideG = 0xf2a6,
        Globe = 0xf0ac,
        GlobeAfrica = 0xf57c,
        GlobeAmericas = 0xf57d,
        GlobeAsia = 0xf57e,
        GlobeEurope = 0xf7a2,
        Gofore = 0xf3a7,
        GolfBall = 0xf450,
        Goodreads = 0xf3a8,
        GoodreadsG = 0xf3a9,
        Google = 0xf1a0,
        GoogleDrive = 0xf3aa,
        GooglePay = 0xe079,
        GooglePlay = 0xf3ab,
        GooglePlus = 0xf2b3,
        GooglePlusG = 0xf0d5,
        GooglePlusSquare = 0xf0d4,
        GoogleWallet = 0xf1ee,
        Gopuram = 0xf664,
        GraduationCap = 0xf19d,
        Gratipay = 0xf184,
        Grav = 0xf2d6,
        GreaterThan = 0xf531,
        GreaterThanEqual = 0xf532,
        Grimace = 0xf57f,
        Grin = 0xf580,
        GrinAlt = 0xf581,
        GrinBeam = 0xf582,
        GrinBeamSweat = 0xf583,
        GrinHearts = 0xf584,
        GrinSquint = 0xf585,
        GrinSquintTears = 0xf586,
        GrinStars = 0xf587,
        GrinTears = 0xf588,
        GrinTongue = 0xf589,
        GrinTongueSquint = 0xf58a,
        GrinTongueWink = 0xf58b,
        GrinWink = 0xf58c,
        Gripfire = 0xf3ac,
        GripHorizontal = 0xf58d,
        GripLines = 0xf7a4,
        GripLinesVertical = 0xf7a5,
        GripVertical = 0xf58e,
        Grunt = 0xf3ad,
        Guilded = 0xe07e,
        Guitar = 0xf7a6,
        Gulp = 0xf3ae,
        HackerNews = 0xf1d4,
        HackerNewsSquare = 0xf3af,
        Hackerrank = 0xf5f7,
        Hamburger = 0xf805,
        Hammer = 0xf6e3,
        Hamsa = 0xf665,
        HandHolding = 0xf4bd,
        HandHoldingHeart = 0xf4be,
        HandHoldingMedical = 0xe05c,
        HandHoldingUsd = 0xf4c0,
        HandHoldingWater = 0xf4c1,
        HandLizard = 0xf258,
        HandMiddleFinger = 0xf806,
        HandPaper = 0xf256,
        HandPeace = 0xf25b,
        HandPointDown = 0xf0a7,
        HandPointer = 0xf25a,
        HandPointLeft = 0xf0a5,
        HandPointRight = 0xf0a4,
        HandPointUp = 0xf0a6,
        HandRock = 0xf255,
        Hands = 0xf4c2,
        HandScissors = 0xf257,
        Handshake = 0xf2b5,
        HandshakeAltSlash = 0xe05f,
        HandshakeSlash = 0xe060,
        HandsHelping = 0xf4c4,
        HandSparkles = 0xe05d,
        HandSpock = 0xf259,
        HandsWash = 0xe05e,
        Hanukiah = 0xf6e6,
        HardHat = 0xf807,
        Hashtag = 0xf292,
        HatCowboy = 0xf8c0,
        HatCowboySide = 0xf8c1,
        HatWizard = 0xf6e8,
        Hdd = 0xf0a0,
        Heading = 0xf1dc,
        Headphones = 0xf025,
        HeadphonesAlt = 0xf58f,
        Headset = 0xf590,
        HeadSideCough = 0xe061,
        HeadSideCoughSlash = 0xe062,
        HeadSideMask = 0xe063,
        HeadSideVirus = 0xe064,
        Heart = 0xf004,
        Heartbeat = 0xf21e,
        HeartBroken = 0xf7a9,
        Helicopter = 0xf533,
        Highlighter = 0xf591,
        Hiking = 0xf6ec,
        Hippo = 0xf6ed,
        Hips = 0xf452,
        HireAHelper = 0xf3b0,
        History = 0xf1da,
        Hive = 0xe07f,
        HockeyPuck = 0xf453,
        HollyBerry = 0xf7aa,
        Home = 0xf015,
        Hooli = 0xf427,
        Hornbill = 0xf592,
        Horse = 0xf6f0,
        HorseHead = 0xf7ab,
        Hospital = 0xf0f8,
        HospitalAlt = 0xf47d,
        HospitalSymbol = 0xf47e,
        HospitalUser = 0xf80d,
        Hotdog = 0xf80f,
        Hotel = 0xf594,
        Hotjar = 0xf3b1,
        HotTub = 0xf593,
        Hourglass = 0xf254,
        HourglassEnd = 0xf253,
        HourglassHalf = 0xf252,
        HourglassStart = 0xf251,
        HouseDamage = 0xf6f1,
        HouseUser = 0xe065,
        Houzz = 0xf27c,
        Hryvnia = 0xf6f2,
        HSquare = 0xf0fd,
        Html5 = 0xf13b,
        Hubspot = 0xf3b2,
        IceCream = 0xf810,
        Icicles = 0xf7ad,
        Icons = 0xf86d,
        ICursor = 0xf246,
        IdBadge = 0xf2c1,
        IdCard = 0xf2c2,
        IdCardAlt = 0xf47f,
        Ideal = 0xe013,
        Igloo = 0xf7ae,
        Image = 0xf03e,
        Images = 0xf302,
        Imdb = 0xf2d8,
        Inbox = 0xf01c,
        Indent = 0xf03c,
        Industry = 0xf275,
        Infinity = 0xf534,
        Info = 0xf129,
        InfoCircle = 0xf05a,
        Innosoft = 0xe080,
        Instagram = 0xf16d,
        InstagramSquare = 0xe055,
        Instalod = 0xe081,
        Intercom = 0xf7af,
        InternetExplorer = 0xf26b,
        Invision = 0xf7b0,
        Ioxhost = 0xf208,
        Italic = 0xf033,
        ItchIo = 0xf83a,
        Itunes = 0xf3b4,
        ItunesNote = 0xf3b5,
        Java = 0xf4e4,
        Jedi = 0xf669,
        JediOrder = 0xf50e,
        Jenkins = 0xf3b6,
        Jira = 0xf7b1,
        Joget = 0xf3b7,
        Joint = 0xf595,
        Joomla = 0xf1aa,
        JournalWhills = 0xf66a,
        Js = 0xf3b8,
        Jsfiddle = 0xf1cc,
        JsSquare = 0xf3b9,
        Kaaba = 0xf66b,
        Kaggle = 0xf5fa,
        Key = 0xf084,
        Keybase = 0xf4f5,
        Keyboard = 0xf11c,
        Keycdn = 0xf3ba,
        Khanda = 0xf66d,
        Kickstarter = 0xf3bb,
        KickstarterK = 0xf3bc,
        Kiss = 0xf596,
        KissBeam = 0xf597,
        KissWinkHeart = 0xf598,
        KiwiBird = 0xf535,
        Korvue = 0xf42f,
        Landmark = 0xf66f,
        Language = 0xf1ab,
        Laptop = 0xf109,
        LaptopCode = 0xf5fc,
        LaptopHouse = 0xe066,
        LaptopMedical = 0xf812,
        Laravel = 0xf3bd,
        Lastfm = 0xf202,
        LastfmSquare = 0xf203,
        Laugh = 0xf599,
        LaughBeam = 0xf59a,
        LaughSquint = 0xf59b,
        LaughWink = 0xf59c,
        LayerGroup = 0xf5fd,
        Leaf = 0xf06c,
        Leanpub = 0xf212,
        Lemon = 0xf094,
        Less = 0xf41d,
        LessThan = 0xf536,
        LessThanEqual = 0xf537,
        LevelDownAlt = 0xf3be,
        LevelUpAlt = 0xf3bf,
        LifeRing = 0xf1cd,
        Lightbulb = 0xf0eb,
        Line = 0xf3c0,
        Link = 0xf0c1,
        Linkedin = 0xf08c,
        LinkedinIn = 0xf0e1,
        Linode = 0xf2b8,
        Linux = 0xf17c,
        LiraSign = 0xf195,
        List = 0xf03a,
        ListAlt = 0xf022,
        ListOl = 0xf0cb,
        ListUl = 0xf0ca,
        LocationArrow = 0xf124,
        Lock = 0xf023,
        LockOpen = 0xf3c1,
        LongArrowAltDown = 0xf309,
        LongArrowAltLeft = 0xf30a,
        LongArrowAltRight = 0xf30b,
        LongArrowAltUp = 0xf30c,
        LowVision = 0xf2a8,
        LuggageCart = 0xf59d,
        Lungs = 0xf604,
        LungsVirus = 0xe067,
        Lyft = 0xf3c3,
        Magento = 0xf3c4,
        Magic = 0xf0d0,
        Magnet = 0xf076,
        MailBulk = 0xf674,
        Mailchimp = 0xf59e,
        Male = 0xf183,
        Mandalorian = 0xf50f,
        Map = 0xf279,
        MapMarked = 0xf59f,
        MapMarkedAlt = 0xf5a0,
        MapMarker = 0xf041,
        MapMarkerAlt = 0xf3c5,
        MapPin = 0xf276,
        MapSigns = 0xf277,
        Markdown = 0xf60f,
        Marker = 0xf5a1,
        Mars = 0xf222,
        MarsDouble = 0xf227,
        MarsStroke = 0xf229,
        MarsStrokeH = 0xf22b,
        MarsStrokeV = 0xf22a,
        Mask = 0xf6fa,
        Mastodon = 0xf4f6,
        Maxcdn = 0xf136,
        Mdb = 0xf8ca,
        Medal = 0xf5a2,
        Medapps = 0xf3c6,
        Medium = 0xf23a,
        MediumM = 0xf3c7,
        Medkit = 0xf0fa,
        Medrt = 0xf3c8,
        Meetup = 0xf2e0,
        Megaport = 0xf5a3,
        Meh = 0xf11a,
        MehBlank = 0xf5a4,
        MehRollingEyes = 0xf5a5,
        Memory = 0xf538,
        Mendeley = 0xf7b3,
        Menorah = 0xf676,
        Mercury = 0xf223,
        Meteor = 0xf753,
        Microblog = 0xe01a,
        Microchip = 0xf2db,
        Microphone = 0xf130,
        MicrophoneAlt = 0xf3c9,
        MicrophoneAltSlash = 0xf539,
        MicrophoneSlash = 0xf131,
        Microscope = 0xf610,
        Microsoft = 0xf3ca,
        Minus = 0xf068,
        MinusCircle = 0xf056,
        MinusSquare = 0xf146,
        Mitten = 0xf7b5,
        Mix = 0xf3cb,
        Mixcloud = 0xf289,
        Mixer = 0xe056,
        Mizuni = 0xf3cc,
        Mobile = 0xf10b,
        MobileAlt = 0xf3cd,
        Modx = 0xf285,
        Monero = 0xf3d0,
        MoneyBill = 0xf0d6,
        MoneyBillAlt = 0xf3d1,
        MoneyBillWave = 0xf53a,
        MoneyBillWaveAlt = 0xf53b,
        MoneyCheck = 0xf53c,
        MoneyCheckAlt = 0xf53d,
        Monument = 0xf5a6,
        Moon = 0xf186,
        MortarPestle = 0xf5a7,
        Mosque = 0xf678,
        Motorcycle = 0xf21c,
        Mountain = 0xf6fc,
        Mouse = 0xf8cc,
        MousePointer = 0xf245,
        MugHot = 0xf7b6,
        Music = 0xf001,
        Napster = 0xf3d2,
        Neos = 0xf612,
        NetworkWired = 0xf6ff,
        Neuter = 0xf22c,
        Newspaper = 0xf1ea,
        Nimblr = 0xf5a8,
        Node = 0xf419,
        NodeJs = 0xf3d3,
        NotEqual = 0xf53e,
        NotesMedical = 0xf481,
        Npm = 0xf3d4,
        Ns8 = 0xf3d5,
        Nutritionix = 0xf3d6,
        ObjectGroup = 0xf247,
        ObjectUngroup = 0xf248,
        OctopusDeploy = 0xe082,
        Odnoklassniki = 0xf263,
        OdnoklassnikiSquare = 0xf264,
        OilCan = 0xf613,
        OldRepublic = 0xf510,
        Om = 0xf679,
        Opencart = 0xf23d,
        Openid = 0xf19b,
        Opera = 0xf26a,
        OptinMonster = 0xf23c,
        Orcid = 0xf8d2,
        Osi = 0xf41a,
        Otter = 0xf700,
        Outdent = 0xf03b,
        Page4 = 0xf3d7,
        Pagelines = 0xf18c,
        Pager = 0xf815,
        PaintBrush = 0xf1fc,
        PaintRoller = 0xf5aa,
        Palette = 0xf53f,
        Palfed = 0xf3d8,
        Pallet = 0xf482,
        Paperclip = 0xf0c6,
        PaperPlane = 0xf1d8,
        ParachuteBox = 0xf4cd,
        Paragraph = 0xf1dd,
        Parking = 0xf540,
        Passport = 0xf5ab,
        Pastafarianism = 0xf67b,
        Paste = 0xf0ea,
        Patreon = 0xf3d9,
        Pause = 0xf04c,
        PauseCircle = 0xf28b,
        Paw = 0xf1b0,
        Paypal = 0xf1ed,
        Peace = 0xf67c,
        Pen = 0xf304,
        PenAlt = 0xf305,
        PencilAlt = 0xf303,
        PencilRuler = 0xf5ae,
        PenFancy = 0xf5ac,
        PenNib = 0xf5ad,
        PennyArcade = 0xf704,
        PenSquare = 0xf14b,
        PeopleArrows = 0xe068,
        PeopleCarry = 0xf4ce,
        PepperHot = 0xf816,
        Perbyte = 0xe083,
        Percent = 0xf295,
        Percentage = 0xf541,
        Periscope = 0xf3da,
        PersonBooth = 0xf756,
        Phabricator = 0xf3db,
        PhoenixFramework = 0xf3dc,
        PhoenixSquadron = 0xf511,
        Phone = 0xf095,
        PhoneAlt = 0xf879,
        PhoneSlash = 0xf3dd,
        PhoneSquare = 0xf098,
        PhoneSquareAlt = 0xf87b,
        PhoneVolume = 0xf2a0,
        PhotoVideo = 0xf87c,
        Php = 0xf457,
        PiedPiper = 0xf2ae,
        PiedPiperAlt = 0xf1a8,
        PiedPiperHat = 0xf4e5,
        PiedPiperPp = 0xf1a7,
        PiedPiperSquare = 0xe01e,
        PiggyBank = 0xf4d3,
        Pills = 0xf484,
        Pinterest = 0xf0d2,
        PinterestP = 0xf231,
        PinterestSquare = 0xf0d3,
        PizzaSlice = 0xf818,
        PlaceOfWorship = 0xf67f,
        Plane = 0xf072,
        PlaneArrival = 0xf5af,
        PlaneDeparture = 0xf5b0,
        PlaneSlash = 0xe069,
        Play = 0xf04b,
        PlayCircle = 0xf144,
        Playstation = 0xf3df,
        Plug = 0xf1e6,
        Plus = 0xf067,
        PlusCircle = 0xf055,
        PlusSquare = 0xf0fe,
        Podcast = 0xf2ce,
        Poll = 0xf681,
        PollH = 0xf682,
        Poo = 0xf2fe,
        Poop = 0xf619,
        PooStorm = 0xf75a,
        Portrait = 0xf3e0,
        PoundSign = 0xf154,
        PowerOff = 0xf011,
        Pray = 0xf683,
        PrayingHands = 0xf684,
        Prescription = 0xf5b1,
        PrescriptionBottle = 0xf485,
        PrescriptionBottleAlt = 0xf486,
        Print = 0xf02f,
        Procedures = 0xf487,
        ProductHunt = 0xf288,
        ProjectDiagram = 0xf542,
        PumpMedical = 0xe06a,
        PumpSoap = 0xe06b,
        Pushed = 0xf3e1,
        PuzzlePiece = 0xf12e,
        Python = 0xf3e2,
        Qq = 0xf1d6,
        Qrcode = 0xf029,
        Question = 0xf128,
        QuestionCircle = 0xf059,
        Quidditch = 0xf458,
        Quinscape = 0xf459,
        Quora = 0xf2c4,
        QuoteLeft = 0xf10d,
        QuoteRight = 0xf10e,
        Quran = 0xf687,
        Radiation = 0xf7b9,
        RadiationAlt = 0xf7ba,
        Rainbow = 0xf75b,
        Random = 0xf074,
        RaspberryPi = 0xf7bb,
        Ravelry = 0xf2d9,
        React = 0xf41b,
        Reacteurope = 0xf75d,
        Readme = 0xf4d5,
        Rebel = 0xf1d0,
        Receipt = 0xf543,
        RecordVinyl = 0xf8d9,
        Recycle = 0xf1b8,
        Reddit = 0xf1a1,
        RedditAlien = 0xf281,
        RedditSquare = 0xf1a2,
        Redhat = 0xf7bc,
        Redo = 0xf01e,
        RedoAlt = 0xf2f9,
        RedRiver = 0xf3e3,
        Registered = 0xf25d,
        RemoveFormat = 0xf87d,
        Renren = 0xf18b,
        Reply = 0xf3e5,
        ReplyAll = 0xf122,
        Replyd = 0xf3e6,
        Republican = 0xf75e,
        Researchgate = 0xf4f8,
        Resolving = 0xf3e7,
        Restroom = 0xf7bd,
        Retweet = 0xf079,
        Rev = 0xf5b2,
        Ribbon = 0xf4d6,
        Ring = 0xf70b,
        Road = 0xf018,
        Robot = 0xf544,
        Rocket = 0xf135,
        Rocketchat = 0xf3e8,
        Rockrms = 0xf3e9,
        Route = 0xf4d7,
        RProject = 0xf4f7,
        Rss = 0xf09e,
        RssSquare = 0xf143,
        RubleSign = 0xf158,
        Ruler = 0xf545,
        RulerCombined = 0xf546,
        RulerHorizontal = 0xf547,
        RulerVertical = 0xf548,
        Running = 0xf70c,
        RupeeSign = 0xf156,
        Rust = 0xe07a,
        SadCry = 0xf5b3,
        SadTear = 0xf5b4,
        Safari = 0xf267,
        Salesforce = 0xf83b,
        Sass = 0xf41e,
        Satellite = 0xf7bf,
        SatelliteDish = 0xf7c0,
        Save = 0xf0c7,
        Schlix = 0xf3ea,
        School = 0xf549,
        Screwdriver = 0xf54a,
        Scribd = 0xf28a,
        Scroll = 0xf70e,
        SdCard = 0xf7c2,
        Search = 0xf002,
        SearchDollar = 0xf688,
        Searchengin = 0xf3eb,
        SearchLocation = 0xf689,
        SearchMinus = 0xf010,
        SearchPlus = 0xf00e,
        Seedling = 0xf4d8,
        Sellcast = 0xf2da,
        Sellsy = 0xf213,
        Server = 0xf233,
        Servicestack = 0xf3ec,
        Shapes = 0xf61f,
        Share = 0xf064,
        ShareAlt = 0xf1e0,
        ShareAltSquare = 0xf1e1,
        ShareSquare = 0xf14d,
        ShekelSign = 0xf20b,
        ShieldAlt = 0xf3ed,
        ShieldVirus = 0xe06c,
        Ship = 0xf21a,
        ShippingFast = 0xf48b,
        Shirtsinbulk = 0xf214,
        ShoePrints = 0xf54b,
        Shopify = 0xe057,
        ShoppingBag = 0xf290,
        ShoppingBasket = 0xf291,
        ShoppingCart = 0xf07a,
        Shopware = 0xf5b5,
        Shower = 0xf2cc,
        ShuttleVan = 0xf5b6,
        Sign = 0xf4d9,
        Signal = 0xf012,
        Signature = 0xf5b7,
        SignInAlt = 0xf2f6,
        SignLanguage = 0xf2a7,
        SignOutAlt = 0xf2f5,
        SimCard = 0xf7c4,
        Simplybuilt = 0xf215,
        Sink = 0xe06d,
        Sistrix = 0xf3ee,
        Sitemap = 0xf0e8,
        Sith = 0xf512,
        Skating = 0xf7c5,
        Sketch = 0xf7c6,
        Skiing = 0xf7c9,
        SkiingNordic = 0xf7ca,
        Skull = 0xf54c,
        SkullCrossbones = 0xf714,
        Skyatlas = 0xf216,
        Skype = 0xf17e,
        Slack = 0xf198,
        SlackHash = 0xf3ef,
        Slash = 0xf715,
        Sleigh = 0xf7cc,
        SlidersH = 0xf1de,
        Slideshare = 0xf1e7,
        Smile = 0xf118,
        SmileBeam = 0xf5b8,
        SmileWink = 0xf4da,
        Smog = 0xf75f,
        Smoking = 0xf48d,
        SmokingBan = 0xf54d,
        Sms = 0xf7cd,
        Snapchat = 0xf2ab,
        SnapchatGhost = 0xf2ac,
        SnapchatSquare = 0xf2ad,
        Snowboarding = 0xf7ce,
        Snowflake = 0xf2dc,
        Snowman = 0xf7d0,
        Snowplow = 0xf7d2,
        Soap = 0xe06e,
        Socks = 0xf696,
        SolarPanel = 0xf5ba,
        Sort = 0xf0dc,
        SortAlphaDown = 0xf15d,
        SortAlphaDownAlt = 0xf881,
        SortAlphaUp = 0xf15e,
        SortAlphaUpAlt = 0xf882,
        SortAmountDown = 0xf160,
        SortAmountDownAlt = 0xf884,
        SortAmountUp = 0xf161,
        SortAmountUpAlt = 0xf885,
        SortDown = 0xf0dd,
        SortNumericDown = 0xf162,
        SortNumericDownAlt = 0xf886,
        SortNumericUp = 0xf163,
        SortNumericUpAlt = 0xf887,
        SortUp = 0xf0de,
        Soundcloud = 0xf1be,
        Sourcetree = 0xf7d3,
        Spa = 0xf5bb,
        SpaceShuttle = 0xf197,
        Speakap = 0xf3f3,
        SpeakerDeck = 0xf83c,
        SpellCheck = 0xf891,
        Spider = 0xf717,
        Spinner = 0xf110,
        Splotch = 0xf5bc,
        Spotify = 0xf1bc,
        SprayCan = 0xf5bd,
        Square = 0xf0c8,
        SquareFull = 0xf45c,
        SquareRootAlt = 0xf698,
        Squarespace = 0xf5be,
        StackExchange = 0xf18d,
        StackOverflow = 0xf16c,
        Stackpath = 0xf842,
        Stamp = 0xf5bf,
        Star = 0xf005,
        StarAndCrescent = 0xf699,
        StarHalf = 0xf089,
        StarHalfAlt = 0xf5c0,
        StarOfDavid = 0xf69a,
        StarOfLife = 0xf621,
        Staylinked = 0xf3f5,
        Steam = 0xf1b6,
        SteamSquare = 0xf1b7,
        SteamSymbol = 0xf3f6,
        StepBackward = 0xf048,
        StepForward = 0xf051,
        Stethoscope = 0xf0f1,
        StickerMule = 0xf3f7,
        StickyNote = 0xf249,
        Stop = 0xf04d,
        StopCircle = 0xf28d,
        Stopwatch = 0xf2f2,
        Stopwatch20 = 0xe06f,
        Store = 0xf54e,
        StoreAlt = 0xf54f,
        StoreAltSlash = 0xe070,
        StoreSlash = 0xe071,
        Strava = 0xf428,
        Stream = 0xf550,
        StreetView = 0xf21d,
        Strikethrough = 0xf0cc,
        Stripe = 0xf429,
        StripeS = 0xf42a,
        Stroopwafel = 0xf551,
        Studiovinari = 0xf3f8,
        Stumbleupon = 0xf1a4,
        StumbleuponCircle = 0xf1a3,
        Subscript = 0xf12c,
        Subway = 0xf239,
        Suitcase = 0xf0f2,
        SuitcaseRolling = 0xf5c1,
        Sun = 0xf185,
        Superpowers = 0xf2dd,
        Superscript = 0xf12b,
        Supple = 0xf3f9,
        Surprise = 0xf5c2,
        Suse = 0xf7d6,
        Swatchbook = 0xf5c3,
        Swift = 0xf8e1,
        Swimmer = 0xf5c4,
        SwimmingPool = 0xf5c5,
        Symfony = 0xf83d,
        Synagogue = 0xf69b,
        Sync = 0xf021,
        SyncAlt = 0xf2f1,
        Syringe = 0xf48e,
        Table = 0xf0ce,
        Tablet = 0xf10a,
        TabletAlt = 0xf3fa,
        TableTennis = 0xf45d,
        Tablets = 0xf490,
        TachometerAlt = 0xf3fd,
        Tag = 0xf02b,
        Tags = 0xf02c,
        Tape = 0xf4db,
        Tasks = 0xf0ae,
        Taxi = 0xf1ba,
        Teamspeak = 0xf4f9,
        Teeth = 0xf62e,
        TeethOpen = 0xf62f,
        Telegram = 0xf2c6,
        TelegramPlane = 0xf3fe,
        TemperatureHigh = 0xf769,
        TemperatureLow = 0xf76b,
        TencentWeibo = 0xf1d5,
        Tenge = 0xf7d7,
        Terminal = 0xf120,
        TextHeight = 0xf034,
        TextWidth = 0xf035,
        Th = 0xf00a,
        TheaterMasks = 0xf630,
        Themeco = 0xf5c6,
        Themeisle = 0xf2b2,
        TheRedYeti = 0xf69d,
        Thermometer = 0xf491,
        ThermometerEmpty = 0xf2cb,
        ThermometerFull = 0xf2c7,
        ThermometerHalf = 0xf2c9,
        ThermometerQuarter = 0xf2ca,
        ThermometerThreeQuarters = 0xf2c8,
        ThinkPeaks = 0xf731,
        ThLarge = 0xf009,
        ThList = 0xf00b,
        ThumbsDown = 0xf165,
        ThumbsUp = 0xf164,
        Thumbtack = 0xf08d,
        TicketAlt = 0xf3ff,
        Tiktok = 0xe07b,
        Times = 0xf00d,
        TimesCircle = 0xf057,
        Tint = 0xf043,
        TintSlash = 0xf5c7,
        Tired = 0xf5c8,
        ToggleOff = 0xf204,
        ToggleOn = 0xf205,
        Toilet = 0xf7d8,
        ToiletPaper = 0xf71e,
        ToiletPaperSlash = 0xe072,
        Toolbox = 0xf552,
        Tools = 0xf7d9,
        Tooth = 0xf5c9,
        Torah = 0xf6a0,
        ToriiGate = 0xf6a1,
        Tractor = 0xf722,
        TradeFederation = 0xf513,
        Trademark = 0xf25c,
        TrafficLight = 0xf637,
        Trailer = 0xe041,
        Train = 0xf238,
        Tram = 0xf7da,
        Transgender = 0xf224,
        TransgenderAlt = 0xf225,
        Trash = 0xf1f8,
        TrashAlt = 0xf2ed,
        TrashRestore = 0xf829,
        TrashRestoreAlt = 0xf82a,
        Tree = 0xf1bb,
        Trello = 0xf181,
        Tripadvisor = 0xf262,
        Trophy = 0xf091,
        Truck = 0xf0d1,
        TruckLoading = 0xf4de,
        TruckMonster = 0xf63b,
        TruckMoving = 0xf4df,
        TruckPickup = 0xf63c,
        Tshirt = 0xf553,
        Tty = 0xf1e4,
        Tumblr = 0xf173,
        TumblrSquare = 0xf174,
        Tv = 0xf26c,
        Twitch = 0xf1e8,
        Twitter = 0xf099,
        TwitterSquare = 0xf081,
        Typo3 = 0xf42b,
        Uber = 0xf402,
        Ubuntu = 0xf7df,
        Uikit = 0xf403,
        Umbraco = 0xf8e8,
        Umbrella = 0xf0e9,
        UmbrellaBeach = 0xf5ca,
        Uncharted = 0xe084,
        Underline = 0xf0cd,
        Undo = 0xf0e2,
        UndoAlt = 0xf2ea,
        Uniregistry = 0xf404,
        Unity = 0xe049,
        UniversalAccess = 0xf29a,
        University = 0xf19c,
        Unlink = 0xf127,
        Unlock = 0xf09c,
        UnlockAlt = 0xf13e,
        Unsplash = 0xe07c,
        Untappd = 0xf405,
        Upload = 0xf093,
        Ups = 0xf7e0,
        Usb = 0xf287,
        User = 0xf007,
        UserAlt = 0xf406,
        UserAltSlash = 0xf4fa,
        UserAstronaut = 0xf4fb,
        UserCheck = 0xf4fc,
        UserCircle = 0xf2bd,
        UserClock = 0xf4fd,
        UserCog = 0xf4fe,
        UserEdit = 0xf4ff,
        UserFriends = 0xf500,
        UserGraduate = 0xf501,
        UserInjured = 0xf728,
        UserLock = 0xf502,
        UserMd = 0xf0f0,
        UserMinus = 0xf503,
        UserNinja = 0xf504,
        UserNurse = 0xf82f,
        UserPlus = 0xf234,
        Users = 0xf0c0,
        UsersCog = 0xf509,
        UserSecret = 0xf21b,
        UserShield = 0xf505,
        UserSlash = 0xf506,
        UsersSlash = 0xe073,
        UserTag = 0xf507,
        UserTie = 0xf508,
        UserTimes = 0xf235,
        Usps = 0xf7e1,
        Ussunnah = 0xf407,
        Utensils = 0xf2e7,
        UtensilSpoon = 0xf2e5,
        Vaadin = 0xf408,
        VectorSquare = 0xf5cb,
        Venus = 0xf221,
        VenusDouble = 0xf226,
        VenusMars = 0xf228,
        Vest = 0xe085,
        VestPatches = 0xe086,
        Viacoin = 0xf237,
        Viadeo = 0xf2a9,
        ViadeoSquare = 0xf2aa,
        Vial = 0xf492,
        Vials = 0xf493,
        Viber = 0xf409,
        Video = 0xf03d,
        VideoSlash = 0xf4e2,
        Vihara = 0xf6a7,
        Vimeo = 0xf40a,
        VimeoSquare = 0xf194,
        VimeoV = 0xf27d,
        Vine = 0xf1ca,
        Virus = 0xe074,
        Viruses = 0xe076,
        VirusSlash = 0xe075,
        Vk = 0xf189,
        Vnv = 0xf40b,
        Voicemail = 0xf897,
        VolleyballBall = 0xf45f,
        VolumeDown = 0xf027,
        VolumeMute = 0xf6a9,
        VolumeOff = 0xf026,
        VolumeUp = 0xf028,
        VoteYea = 0xf772,
        VrCardboard = 0xf729,
        Vuejs = 0xf41f,
        Walking = 0xf554,
        Wallet = 0xf555,
        Warehouse = 0xf494,
        WatchmanMonitoring = 0xe087,
        Water = 0xf773,
        WaveSquare = 0xf83e,
        Waze = 0xf83f,
        Weebly = 0xf5cc,
        Weibo = 0xf18a,
        Weight = 0xf496,
        WeightHanging = 0xf5cd,
        Weixin = 0xf1d7,
        Whatsapp = 0xf232,
        WhatsappSquare = 0xf40c,
        Wheelchair = 0xf193,
        Whmcs = 0xf40d,
        Wifi = 0xf1eb,
        WikipediaW = 0xf266,
        Wind = 0xf72e,
        WindowClose = 0xf410,
        WindowMaximize = 0xf2d0,
        WindowMinimize = 0xf2d1,
        WindowRestore = 0xf2d2,
        Windows = 0xf17a,
        WineBottle = 0xf72f,
        WineGlass = 0xf4e3,
        WineGlassAlt = 0xf5ce,
        Wix = 0xf5cf,
        WizardsOfTheCoast = 0xf730,
        Wodu = 0xe088,
        WolfPackBattalion = 0xf514,
        WonSign = 0xf159,
        Wordpress = 0xf19a,
        WordpressSimple = 0xf411,
        Wpbeginner = 0xf297,
        Wpexplorer = 0xf2de,
        Wpforms = 0xf298,
        Wpressr = 0xf3e4,
        Wrench = 0xf0ad,
        Xbox = 0xf412,
        Xing = 0xf168,
        XingSquare = 0xf169,
        XRay = 0xf497,
        Yahoo = 0xf19e,
        Yammer = 0xf840,
        Yandex = 0xf413,
        YandexInternational = 0xf414,
        Yarn = 0xf7e3,
        YCombinator = 0xf23b,
        Yelp = 0xf1e9,
        YenSign = 0xf157,
        YinYang = 0xf6ad,
        Yoast = 0xf2b1,
        Youtube = 0xf167,
        YoutubeSquare = 0xf431,
        Zhihu = 0xf63f,
    }*/
    #endregion

    internal class FA
    {
        internal static int IconValue(IconFA ico)
        {
            int ret = 0;
            switch (ico)
            {
                case IconFA._None: ret = 0; break;
                case IconFA._500Px: ret = 0xf26e; break;
                case IconFA.AccessibleIcon: ret = 0xf368; break;
                case IconFA.Accusoft: ret = 0xf369; break;
                case IconFA.AcquisitionsIncorporated: ret = 0xf6af; break;
                case IconFA.Ad: ret = 0xf641; break;
                case IconFA.AddressBook: ret = 0xf2b9; break;
                case IconFA.AddressCard: ret = 0xf2bb; break;
                case IconFA.Adjust: ret = 0xf042; break;
                case IconFA.Adn: ret = 0xf170; break;
                case IconFA.Adversal: ret = 0xf36a; break;
                case IconFA.Affiliatetheme: ret = 0xf36b; break;
                case IconFA.Airbnb: ret = 0xf834; break;
                case IconFA.AirFreshener: ret = 0xf5d0; break;
                case IconFA.Algolia: ret = 0xf36c; break;
                case IconFA.AlignCenter: ret = 0xf037; break;
                case IconFA.AlignJustify: ret = 0xf039; break;
                case IconFA.AlignLeft: ret = 0xf036; break;
                case IconFA.AlignRight: ret = 0xf038; break;
                case IconFA.Alipay: ret = 0xf642; break;
                case IconFA.Allergies: ret = 0xf461; break;
                case IconFA.Amazon: ret = 0xf270; break;
                case IconFA.AmazonPay: ret = 0xf42c; break;
                case IconFA.Ambulance: ret = 0xf0f9; break;
                case IconFA.AmericanSignLanguageInterpreting: ret = 0xf2a3; break;
                case IconFA.Amilia: ret = 0xf36d; break;
                case IconFA.Anchor: ret = 0xf13d; break;
                case IconFA.Android: ret = 0xf17b; break;
                case IconFA.Angellist: ret = 0xf209; break;
                case IconFA.AngleDoubleDown: ret = 0xf103; break;
                case IconFA.AngleDoubleLeft: ret = 0xf100; break;
                case IconFA.AngleDoubleRight: ret = 0xf101; break;
                case IconFA.AngleDoubleUp: ret = 0xf102; break;
                case IconFA.AngleDown: ret = 0xf107; break;
                case IconFA.AngleLeft: ret = 0xf104; break;
                case IconFA.AngleRight: ret = 0xf105; break;
                case IconFA.AngleUp: ret = 0xf106; break;
                case IconFA.Angry: ret = 0xf556; break;
                case IconFA.Angrycreative: ret = 0xf36e; break;
                case IconFA.Angular: ret = 0xf420; break;
                case IconFA.Ankh: ret = 0xf644; break;
                case IconFA.Apper: ret = 0xf371; break;
                case IconFA.Apple: ret = 0xf179; break;
                case IconFA.AppleAlt: ret = 0xf5d1; break;
                case IconFA.ApplePay: ret = 0xf415; break;
                case IconFA.AppStore: ret = 0xf36f; break;
                case IconFA.AppStoreIos: ret = 0xf370; break;
                case IconFA.Archive: ret = 0xf187; break;
                case IconFA.Archway: ret = 0xf557; break;
                case IconFA.ArrowAltCircleDown: ret = 0xf358; break;
                case IconFA.ArrowAltCircleLeft: ret = 0xf359; break;
                case IconFA.ArrowAltCircleRight: ret = 0xf35a; break;
                case IconFA.ArrowAltCircleUp: ret = 0xf35b; break;
                case IconFA.ArrowCircleDown: ret = 0xf0ab; break;
                case IconFA.ArrowCircleLeft: ret = 0xf0a8; break;
                case IconFA.ArrowCircleRight: ret = 0xf0a9; break;
                case IconFA.ArrowCircleUp: ret = 0xf0aa; break;
                case IconFA.ArrowDown: ret = 0xf063; break;
                case IconFA.ArrowLeft: ret = 0xf060; break;
                case IconFA.ArrowRight: ret = 0xf061; break;
                case IconFA.ArrowsAlt: ret = 0xf0b2; break;
                case IconFA.ArrowsAltH: ret = 0xf337; break;
                case IconFA.ArrowsAltV: ret = 0xf338; break;
                case IconFA.ArrowUp: ret = 0xf062; break;
                case IconFA.Artstation: ret = 0xf77a; break;
                case IconFA.AssistiveListeningSystems: ret = 0xf2a2; break;
                case IconFA.Asterisk: ret = 0xf069; break;
                case IconFA.Asymmetrik: ret = 0xf372; break;
                case IconFA.At: ret = 0xf1fa; break;
                case IconFA.Atlas: ret = 0xf558; break;
                case IconFA.Atlassian: ret = 0xf77b; break;
                case IconFA.Atom: ret = 0xf5d2; break;
                case IconFA.Audible: ret = 0xf373; break;
                case IconFA.AudioDescription: ret = 0xf29e; break;
                case IconFA.Autoprefixer: ret = 0xf41c; break;
                case IconFA.Avianex: ret = 0xf374; break;
                case IconFA.Aviato: ret = 0xf421; break;
                case IconFA.Award: ret = 0xf559; break;
                case IconFA.Aws: ret = 0xf375; break;
                case IconFA.Baby: ret = 0xf77c; break;
                case IconFA.BabyCarriage: ret = 0xf77d; break;
                case IconFA.Backspace: ret = 0xf55a; break;
                case IconFA.Backward: ret = 0xf04a; break;
                case IconFA.Bacon: ret = 0xf7e5; break;
                case IconFA.Bacteria: ret = 0xe059; break;
                case IconFA.Bacterium: ret = 0xe05a; break;
                case IconFA.Bahai: ret = 0xf666; break;
                case IconFA.BalanceScale: ret = 0xf24e; break;
                case IconFA.BalanceScaleLeft: ret = 0xf515; break;
                case IconFA.BalanceScaleRight: ret = 0xf516; break;
                case IconFA.Ban: ret = 0xf05e; break;
                case IconFA.BandAid: ret = 0xf462; break;
                case IconFA.Bandcamp: ret = 0xf2d5; break;
                case IconFA.Barcode: ret = 0xf02a; break;
                case IconFA.Bars: ret = 0xf0c9; break;
                case IconFA.BaseballBall: ret = 0xf433; break;
                case IconFA.BasketballBall: ret = 0xf434; break;
                case IconFA.Bath: ret = 0xf2cd; break;
                case IconFA.BatteryEmpty: ret = 0xf244; break;
                case IconFA.BatteryFull: ret = 0xf240; break;
                case IconFA.BatteryHalf: ret = 0xf242; break;
                case IconFA.BatteryQuarter: ret = 0xf243; break;
                case IconFA.BatteryThreeQuarters: ret = 0xf241; break;
                case IconFA.BattleNet: ret = 0xf835; break;
                case IconFA.Bed: ret = 0xf236; break;
                case IconFA.Beer: ret = 0xf0fc; break;
                case IconFA.Behance: ret = 0xf1b4; break;
                case IconFA.BehanceSquare: ret = 0xf1b5; break;
                case IconFA.Bell: ret = 0xf0f3; break;
                case IconFA.BellSlash: ret = 0xf1f6; break;
                case IconFA.BezierCurve: ret = 0xf55b; break;
                case IconFA.Bible: ret = 0xf647; break;
                case IconFA.Bicycle: ret = 0xf206; break;
                case IconFA.Biking: ret = 0xf84a; break;
                case IconFA.Bimobject: ret = 0xf378; break;
                case IconFA.Binoculars: ret = 0xf1e5; break;
                case IconFA.Biohazard: ret = 0xf780; break;
                case IconFA.BirthdayCake: ret = 0xf1fd; break;
                case IconFA.Bitbucket: ret = 0xf171; break;
                case IconFA.Bitcoin: ret = 0xf379; break;
                case IconFA.Bity: ret = 0xf37a; break;
                case IconFA.Blackberry: ret = 0xf37b; break;
                case IconFA.BlackTie: ret = 0xf27e; break;
                case IconFA.Blender: ret = 0xf517; break;
                case IconFA.BlenderPhone: ret = 0xf6b6; break;
                case IconFA.Blind: ret = 0xf29d; break;
                case IconFA.Blog: ret = 0xf781; break;
                case IconFA.Blogger: ret = 0xf37c; break;
                case IconFA.BloggerB: ret = 0xf37d; break;
                case IconFA.Bluetooth: ret = 0xf293; break;
                case IconFA.BluetoothB: ret = 0xf294; break;
                case IconFA.Bold: ret = 0xf032; break;
                case IconFA.Bolt: ret = 0xf0e7; break;
                case IconFA.Bomb: ret = 0xf1e2; break;
                case IconFA.Bone: ret = 0xf5d7; break;
                case IconFA.Bong: ret = 0xf55c; break;
                case IconFA.Book: ret = 0xf02d; break;
                case IconFA.BookDead: ret = 0xf6b7; break;
                case IconFA.Bookmark: ret = 0xf02e; break;
                case IconFA.BookMedical: ret = 0xf7e6; break;
                case IconFA.BookOpen: ret = 0xf518; break;
                case IconFA.BookReader: ret = 0xf5da; break;
                case IconFA.Bootstrap: ret = 0xf836; break;
                case IconFA.BorderAll: ret = 0xf84c; break;
                case IconFA.BorderNone: ret = 0xf850; break;
                case IconFA.BorderStyle: ret = 0xf853; break;
                case IconFA.BowlingBall: ret = 0xf436; break;
                case IconFA.Box: ret = 0xf466; break;
                case IconFA.Boxes: ret = 0xf468; break;
                case IconFA.BoxOpen: ret = 0xf49e; break;
                case IconFA.BoxTissue: ret = 0xe05b; break;
                case IconFA.Braille: ret = 0xf2a1; break;
                case IconFA.Brain: ret = 0xf5dc; break;
                case IconFA.BreadSlice: ret = 0xf7ec; break;
                case IconFA.Briefcase: ret = 0xf0b1; break;
                case IconFA.BriefcaseMedical: ret = 0xf469; break;
                case IconFA.BroadcastTower: ret = 0xf519; break;
                case IconFA.Broom: ret = 0xf51a; break;
                case IconFA.Brush: ret = 0xf55d; break;
                case IconFA.Btc: ret = 0xf15a; break;
                case IconFA.Buffer: ret = 0xf837; break;
                case IconFA.Bug: ret = 0xf188; break;
                case IconFA.Building: ret = 0xf1ad; break;
                case IconFA.Bullhorn: ret = 0xf0a1; break;
                case IconFA.Bullseye: ret = 0xf140; break;
                case IconFA.Burn: ret = 0xf46a; break;
                case IconFA.Buromobelexperte: ret = 0xf37f; break;
                case IconFA.Bus: ret = 0xf207; break;
                case IconFA.BusAlt: ret = 0xf55e; break;
                case IconFA.BusinessTime: ret = 0xf64a; break;
                case IconFA.BuyNLarge: ret = 0xf8a6; break;
                case IconFA.Buysellads: ret = 0xf20d; break;
                case IconFA.Calculator: ret = 0xf1ec; break;
                case IconFA.Calendar: ret = 0xf133; break;
                case IconFA.CalendarAlt: ret = 0xf073; break;
                case IconFA.CalendarCheck: ret = 0xf274; break;
                case IconFA.CalendarDay: ret = 0xf783; break;
                case IconFA.CalendarMinus: ret = 0xf272; break;
                case IconFA.CalendarPlus: ret = 0xf271; break;
                case IconFA.CalendarTimes: ret = 0xf273; break;
                case IconFA.CalendarWeek: ret = 0xf784; break;
                case IconFA.Camera: ret = 0xf030; break;
                case IconFA.CameraRetro: ret = 0xf083; break;
                case IconFA.Campground: ret = 0xf6bb; break;
                case IconFA.CanadianMapleLeaf: ret = 0xf785; break;
                case IconFA.CandyCane: ret = 0xf786; break;
                case IconFA.Cannabis: ret = 0xf55f; break;
                case IconFA.Capsules: ret = 0xf46b; break;
                case IconFA.Car: ret = 0xf1b9; break;
                case IconFA.CarAlt: ret = 0xf5de; break;
                case IconFA.Caravan: ret = 0xf8ff; break;
                case IconFA.CarBattery: ret = 0xf5df; break;
                case IconFA.CarCrash: ret = 0xf5e1; break;
                case IconFA.CaretDown: ret = 0xf0d7; break;
                case IconFA.CaretLeft: ret = 0xf0d9; break;
                case IconFA.CaretRight: ret = 0xf0da; break;
                case IconFA.CaretSquareDown: ret = 0xf150; break;
                case IconFA.CaretSquareLeft: ret = 0xf191; break;
                case IconFA.CaretSquareRight: ret = 0xf152; break;
                case IconFA.CaretSquareUp: ret = 0xf151; break;
                case IconFA.CaretUp: ret = 0xf0d8; break;
                case IconFA.Carrot: ret = 0xf787; break;
                case IconFA.CarSide: ret = 0xf5e4; break;
                case IconFA.CartArrowDown: ret = 0xf218; break;
                case IconFA.CartPlus: ret = 0xf217; break;
                case IconFA.CashRegister: ret = 0xf788; break;
                case IconFA.Cat: ret = 0xf6be; break;
                case IconFA.CcAmazonPay: ret = 0xf42d; break;
                case IconFA.CcAmex: ret = 0xf1f3; break;
                case IconFA.CcApplePay: ret = 0xf416; break;
                case IconFA.CcDinersClub: ret = 0xf24c; break;
                case IconFA.CcDiscover: ret = 0xf1f2; break;
                case IconFA.CcJcb: ret = 0xf24b; break;
                case IconFA.CcMastercard: ret = 0xf1f1; break;
                case IconFA.CcPaypal: ret = 0xf1f4; break;
                case IconFA.CcStripe: ret = 0xf1f5; break;
                case IconFA.CcVisa: ret = 0xf1f0; break;
                case IconFA.Centercode: ret = 0xf380; break;
                case IconFA.Centos: ret = 0xf789; break;
                case IconFA.Certificate: ret = 0xf0a3; break;
                case IconFA.Chair: ret = 0xf6c0; break;
                case IconFA.Chalkboard: ret = 0xf51b; break;
                case IconFA.ChalkboardTeacher: ret = 0xf51c; break;
                case IconFA.ChargingStation: ret = 0xf5e7; break;
                case IconFA.ChartArea: ret = 0xf1fe; break;
                case IconFA.ChartBar: ret = 0xf080; break;
                case IconFA.ChartLine: ret = 0xf201; break;
                case IconFA.ChartPie: ret = 0xf200; break;
                case IconFA.Check: ret = 0xf00c; break;
                case IconFA.CheckCircle: ret = 0xf058; break;
                case IconFA.CheckDouble: ret = 0xf560; break;
                case IconFA.CheckSquare: ret = 0xf14a; break;
                case IconFA.Cheese: ret = 0xf7ef; break;
                case IconFA.Chess: ret = 0xf439; break;
                case IconFA.ChessBishop: ret = 0xf43a; break;
                case IconFA.ChessBoard: ret = 0xf43c; break;
                case IconFA.ChessKing: ret = 0xf43f; break;
                case IconFA.ChessKnight: ret = 0xf441; break;
                case IconFA.ChessPawn: ret = 0xf443; break;
                case IconFA.ChessQueen: ret = 0xf445; break;
                case IconFA.ChessRook: ret = 0xf447; break;
                case IconFA.ChevronCircleDown: ret = 0xf13a; break;
                case IconFA.ChevronCircleLeft: ret = 0xf137; break;
                case IconFA.ChevronCircleRight: ret = 0xf138; break;
                case IconFA.ChevronCircleUp: ret = 0xf139; break;
                case IconFA.ChevronDown: ret = 0xf078; break;
                case IconFA.ChevronLeft: ret = 0xf053; break;
                case IconFA.ChevronRight: ret = 0xf054; break;
                case IconFA.ChevronUp: ret = 0xf077; break;
                case IconFA.Child: ret = 0xf1ae; break;
                case IconFA.Chrome: ret = 0xf268; break;
                case IconFA.Chromecast: ret = 0xf838; break;
                case IconFA.Church: ret = 0xf51d; break;
                case IconFA.Circle: ret = 0xf111; break;
                case IconFA.CircleNotch: ret = 0xf1ce; break;
                case IconFA.City: ret = 0xf64f; break;
                case IconFA.ClinicMedical: ret = 0xf7f2; break;
                case IconFA.Clipboard: ret = 0xf328; break;
                case IconFA.ClipboardCheck: ret = 0xf46c; break;
                case IconFA.ClipboardList: ret = 0xf46d; break;
                case IconFA.Clock: ret = 0xf017; break;
                case IconFA.Clone: ret = 0xf24d; break;
                case IconFA.ClosedCaptioning: ret = 0xf20a; break;
                case IconFA.Cloud: ret = 0xf0c2; break;
                case IconFA.CloudDownloadAlt: ret = 0xf381; break;
                case IconFA.Cloudflare: ret = 0xe07d; break;
                case IconFA.CloudMeatball: ret = 0xf73b; break;
                case IconFA.CloudMoon: ret = 0xf6c3; break;
                case IconFA.CloudMoonRain: ret = 0xf73c; break;
                case IconFA.CloudRain: ret = 0xf73d; break;
                case IconFA.Cloudscale: ret = 0xf383; break;
                case IconFA.CloudShowersHeavy: ret = 0xf740; break;
                case IconFA.Cloudsmith: ret = 0xf384; break;
                case IconFA.CloudSun: ret = 0xf6c4; break;
                case IconFA.CloudSunRain: ret = 0xf743; break;
                case IconFA.CloudUploadAlt: ret = 0xf382; break;
                case IconFA.Cloudversify: ret = 0xf385; break;
                case IconFA.Cocktail: ret = 0xf561; break;
                case IconFA.Code: ret = 0xf121; break;
                case IconFA.CodeBranch: ret = 0xf126; break;
                case IconFA.Codepen: ret = 0xf1cb; break;
                case IconFA.Codiepie: ret = 0xf284; break;
                case IconFA.Coffee: ret = 0xf0f4; break;
                case IconFA.Cog: ret = 0xf013; break;
                case IconFA.Cogs: ret = 0xf085; break;
                case IconFA.Coins: ret = 0xf51e; break;
                case IconFA.Columns: ret = 0xf0db; break;
                case IconFA.Comment: ret = 0xf075; break;
                case IconFA.CommentAlt: ret = 0xf27a; break;
                case IconFA.CommentDollar: ret = 0xf651; break;
                case IconFA.CommentDots: ret = 0xf4ad; break;
                case IconFA.CommentMedical: ret = 0xf7f5; break;
                case IconFA.Comments: ret = 0xf086; break;
                case IconFA.CommentsDollar: ret = 0xf653; break;
                case IconFA.CommentSlash: ret = 0xf4b3; break;
                case IconFA.CompactDisc: ret = 0xf51f; break;
                case IconFA.Compass: ret = 0xf14e; break;
                case IconFA.Compress: ret = 0xf066; break;
                case IconFA.CompressAlt: ret = 0xf422; break;
                case IconFA.CompressArrowsAlt: ret = 0xf78c; break;
                case IconFA.ConciergeBell: ret = 0xf562; break;
                case IconFA.Confluence: ret = 0xf78d; break;
                case IconFA.Connectdevelop: ret = 0xf20e; break;
                case IconFA.Contao: ret = 0xf26d; break;
                case IconFA.Cookie: ret = 0xf563; break;
                case IconFA.CookieBite: ret = 0xf564; break;
                case IconFA.Copy: ret = 0xf0c5; break;
                case IconFA.Copyright: ret = 0xf1f9; break;
                case IconFA.CottonBureau: ret = 0xf89e; break;
                case IconFA.Couch: ret = 0xf4b8; break;
                case IconFA.Cpanel: ret = 0xf388; break;
                case IconFA.CreativeCommons: ret = 0xf25e; break;
                case IconFA.CreativeCommonsBy: ret = 0xf4e7; break;
                case IconFA.CreativeCommonsNc: ret = 0xf4e8; break;
                case IconFA.CreativeCommonsNcEu: ret = 0xf4e9; break;
                case IconFA.CreativeCommonsNcJp: ret = 0xf4ea; break;
                case IconFA.CreativeCommonsNd: ret = 0xf4eb; break;
                case IconFA.CreativeCommonsPd: ret = 0xf4ec; break;
                case IconFA.CreativeCommonsPdAlt: ret = 0xf4ed; break;
                case IconFA.CreativeCommonsRemix: ret = 0xf4ee; break;
                case IconFA.CreativeCommonsSa: ret = 0xf4ef; break;
                case IconFA.CreativeCommonsSampling: ret = 0xf4f0; break;
                case IconFA.CreativeCommonsSamplingPlus: ret = 0xf4f1; break;
                case IconFA.CreativeCommonsShare: ret = 0xf4f2; break;
                case IconFA.CreativeCommonsZero: ret = 0xf4f3; break;
                case IconFA.CreditCard: ret = 0xf09d; break;
                case IconFA.CriticalRole: ret = 0xf6c9; break;
                case IconFA.Crop: ret = 0xf125; break;
                case IconFA.CropAlt: ret = 0xf565; break;
                case IconFA.Cross: ret = 0xf654; break;
                case IconFA.Crosshairs: ret = 0xf05b; break;
                case IconFA.Crow: ret = 0xf520; break;
                case IconFA.Crown: ret = 0xf521; break;
                case IconFA.Crutch: ret = 0xf7f7; break;
                case IconFA.Css3: ret = 0xf13c; break;
                case IconFA.Css3Alt: ret = 0xf38b; break;
                case IconFA.Cube: ret = 0xf1b2; break;
                case IconFA.Cubes: ret = 0xf1b3; break;
                case IconFA.Cut: ret = 0xf0c4; break;
                case IconFA.Cuttlefish: ret = 0xf38c; break;
                case IconFA.Dailymotion: ret = 0xe052; break;
                case IconFA.DAndD: ret = 0xf38d; break;
                case IconFA.DAndDBeyond: ret = 0xf6ca; break;
                case IconFA.Dashcube: ret = 0xf210; break;
                case IconFA.Database: ret = 0xf1c0; break;
                case IconFA.Deaf: ret = 0xf2a4; break;
                case IconFA.Deezer: ret = 0xe077; break;
                case IconFA.Delicious: ret = 0xf1a5; break;
                case IconFA.Democrat: ret = 0xf747; break;
                case IconFA.Deploydog: ret = 0xf38e; break;
                case IconFA.Deskpro: ret = 0xf38f; break;
                case IconFA.Desktop: ret = 0xf108; break;
                case IconFA.Dev: ret = 0xf6cc; break;
                case IconFA.Deviantart: ret = 0xf1bd; break;
                case IconFA.Dharmachakra: ret = 0xf655; break;
                case IconFA.Dhl: ret = 0xf790; break;
                case IconFA.Diagnoses: ret = 0xf470; break;
                case IconFA.Diaspora: ret = 0xf791; break;
                case IconFA.Dice: ret = 0xf522; break;
                case IconFA.DiceD20: ret = 0xf6cf; break;
                case IconFA.DiceD6: ret = 0xf6d1; break;
                case IconFA.DiceFive: ret = 0xf523; break;
                case IconFA.DiceFour: ret = 0xf524; break;
                case IconFA.DiceOne: ret = 0xf525; break;
                case IconFA.DiceSix: ret = 0xf526; break;
                case IconFA.DiceThree: ret = 0xf527; break;
                case IconFA.DiceTwo: ret = 0xf528; break;
                case IconFA.Digg: ret = 0xf1a6; break;
                case IconFA.DigitalOcean: ret = 0xf391; break;
                case IconFA.DigitalTachograph: ret = 0xf566; break;
                case IconFA.Directions: ret = 0xf5eb; break;
                case IconFA.Discord: ret = 0xf392; break;
                case IconFA.Discourse: ret = 0xf393; break;
                case IconFA.Disease: ret = 0xf7fa; break;
                case IconFA.Divide: ret = 0xf529; break;
                case IconFA.Dizzy: ret = 0xf567; break;
                case IconFA.Dna: ret = 0xf471; break;
                case IconFA.Dochub: ret = 0xf394; break;
                case IconFA.Docker: ret = 0xf395; break;
                case IconFA.Dog: ret = 0xf6d3; break;
                case IconFA.DollarSign: ret = 0xf155; break;
                case IconFA.Dolly: ret = 0xf472; break;
                case IconFA.DollyFlatbed: ret = 0xf474; break;
                case IconFA.Donate: ret = 0xf4b9; break;
                case IconFA.DoorClosed: ret = 0xf52a; break;
                case IconFA.DoorOpen: ret = 0xf52b; break;
                case IconFA.DotCircle: ret = 0xf192; break;
                case IconFA.Dove: ret = 0xf4ba; break;
                case IconFA.Download: ret = 0xf019; break;
                case IconFA.Draft2digital: ret = 0xf396; break;
                case IconFA.DraftingCompass: ret = 0xf568; break;
                case IconFA.Dragon: ret = 0xf6d5; break;
                case IconFA.DrawPolygon: ret = 0xf5ee; break;
                case IconFA.Dribbble: ret = 0xf17d; break;
                case IconFA.DribbbleSquare: ret = 0xf397; break;
                case IconFA.Dropbox: ret = 0xf16b; break;
                case IconFA.Drum: ret = 0xf569; break;
                case IconFA.DrumSteelpan: ret = 0xf56a; break;
                case IconFA.DrumstickBite: ret = 0xf6d7; break;
                case IconFA.Drupal: ret = 0xf1a9; break;
                case IconFA.Dumbbell: ret = 0xf44b; break;
                case IconFA.Dumpster: ret = 0xf793; break;
                case IconFA.DumpsterFire: ret = 0xf794; break;
                case IconFA.Dungeon: ret = 0xf6d9; break;
                case IconFA.Dyalog: ret = 0xf399; break;
                case IconFA.Earlybirds: ret = 0xf39a; break;
                case IconFA.Ebay: ret = 0xf4f4; break;
                case IconFA.Edge: ret = 0xf282; break;
                case IconFA.EdgeLegacy: ret = 0xe078; break;
                case IconFA.Edit: ret = 0xf044; break;
                case IconFA.Egg: ret = 0xf7fb; break;
                case IconFA.Eject: ret = 0xf052; break;
                case IconFA.Elementor: ret = 0xf430; break;
                case IconFA.EllipsisH: ret = 0xf141; break;
                case IconFA.EllipsisV: ret = 0xf142; break;
                case IconFA.Ello: ret = 0xf5f1; break;
                case IconFA.Ember: ret = 0xf423; break;
                case IconFA.Empire: ret = 0xf1d1; break;
                case IconFA.Envelope: ret = 0xf0e0; break;
                case IconFA.EnvelopeOpen: ret = 0xf2b6; break;
                case IconFA.EnvelopeOpenText: ret = 0xf658; break;
                case IconFA.EnvelopeSquare: ret = 0xf199; break;
                case IconFA.Envira: ret = 0xf299; break;
                case IconFA.Equals: ret = 0xf52c; break;
                case IconFA.Eraser: ret = 0xf12d; break;
                case IconFA.Erlang: ret = 0xf39d; break;
                case IconFA.Ethereum: ret = 0xf42e; break;
                case IconFA.Ethernet: ret = 0xf796; break;
                case IconFA.Etsy: ret = 0xf2d7; break;
                case IconFA.EuroSign: ret = 0xf153; break;
                case IconFA.Evernote: ret = 0xf839; break;
                case IconFA.ExchangeAlt: ret = 0xf362; break;
                case IconFA.Exclamation: ret = 0xf12a; break;
                case IconFA.ExclamationCircle: ret = 0xf06a; break;
                case IconFA.ExclamationTriangle: ret = 0xf071; break;
                case IconFA.Expand: ret = 0xf065; break;
                case IconFA.ExpandAlt: ret = 0xf424; break;
                case IconFA.ExpandArrowsAlt: ret = 0xf31e; break;
                case IconFA.Expeditedssl: ret = 0xf23e; break;
                case IconFA.ExternalLinkAlt: ret = 0xf35d; break;
                case IconFA.ExternalLinkSquareAlt: ret = 0xf360; break;
                case IconFA.Eye: ret = 0xf06e; break;
                case IconFA.EyeDropper: ret = 0xf1fb; break;
                case IconFA.EyeSlash: ret = 0xf070; break;
                case IconFA.Facebook: ret = 0xf09a; break;
                case IconFA.FacebookF: ret = 0xf39e; break;
                case IconFA.FacebookMessenger: ret = 0xf39f; break;
                case IconFA.FacebookSquare: ret = 0xf082; break;
                case IconFA.Fan: ret = 0xf863; break;
                case IconFA.FantasyFlightGames: ret = 0xf6dc; break;
                case IconFA.FastBackward: ret = 0xf049; break;
                case IconFA.FastForward: ret = 0xf050; break;
                case IconFA.Faucet: ret = 0xe005; break;
                case IconFA.Fax: ret = 0xf1ac; break;
                case IconFA.Feather: ret = 0xf52d; break;
                case IconFA.FeatherAlt: ret = 0xf56b; break;
                case IconFA.Fedex: ret = 0xf797; break;
                case IconFA.Fedora: ret = 0xf798; break;
                case IconFA.Female: ret = 0xf182; break;
                case IconFA.FighterJet: ret = 0xf0fb; break;
                case IconFA.Figma: ret = 0xf799; break;
                case IconFA.File: ret = 0xf15b; break;
                case IconFA.FileAlt: ret = 0xf15c; break;
                case IconFA.FileArchive: ret = 0xf1c6; break;
                case IconFA.FileAudio: ret = 0xf1c7; break;
                case IconFA.FileCode: ret = 0xf1c9; break;
                case IconFA.FileContract: ret = 0xf56c; break;
                case IconFA.FileCsv: ret = 0xf6dd; break;
                case IconFA.FileDownload: ret = 0xf56d; break;
                case IconFA.FileExcel: ret = 0xf1c3; break;
                case IconFA.FileExport: ret = 0xf56e; break;
                case IconFA.FileImage: ret = 0xf1c5; break;
                case IconFA.FileImport: ret = 0xf56f; break;
                case IconFA.FileInvoice: ret = 0xf570; break;
                case IconFA.FileInvoiceDollar: ret = 0xf571; break;
                case IconFA.FileMedical: ret = 0xf477; break;
                case IconFA.FileMedicalAlt: ret = 0xf478; break;
                case IconFA.FilePdf: ret = 0xf1c1; break;
                case IconFA.FilePowerpoint: ret = 0xf1c4; break;
                case IconFA.FilePrescription: ret = 0xf572; break;
                case IconFA.FileSignature: ret = 0xf573; break;
                case IconFA.FileUpload: ret = 0xf574; break;
                case IconFA.FileVideo: ret = 0xf1c8; break;
                case IconFA.FileWord: ret = 0xf1c2; break;
                case IconFA.Fill: ret = 0xf575; break;
                case IconFA.FillDrip: ret = 0xf576; break;
                case IconFA.Film: ret = 0xf008; break;
                case IconFA.Filter: ret = 0xf0b0; break;
                case IconFA.Fingerprint: ret = 0xf577; break;
                case IconFA.Fire: ret = 0xf06d; break;
                case IconFA.FireAlt: ret = 0xf7e4; break;
                case IconFA.FireExtinguisher: ret = 0xf134; break;
                case IconFA.Firefox: ret = 0xf269; break;
                case IconFA.FirefoxBrowser: ret = 0xe007; break;
                case IconFA.FirstAid: ret = 0xf479; break;
                case IconFA.Firstdraft: ret = 0xf3a1; break;
                case IconFA.FirstOrder: ret = 0xf2b0; break;
                case IconFA.FirstOrderAlt: ret = 0xf50a; break;
                case IconFA.Fish: ret = 0xf578; break;
                case IconFA.FistRaised: ret = 0xf6de; break;
                case IconFA.Flag: ret = 0xf024; break;
                case IconFA.FlagCheckered: ret = 0xf11e; break;
                case IconFA.FlagUsa: ret = 0xf74d; break;
                case IconFA.Flask: ret = 0xf0c3; break;
                case IconFA.Flickr: ret = 0xf16e; break;
                case IconFA.Flipboard: ret = 0xf44d; break;
                case IconFA.Flushed: ret = 0xf579; break;
                case IconFA.Fly: ret = 0xf417; break;
                case IconFA.Folder: ret = 0xf07b; break;
                case IconFA.FolderMinus: ret = 0xf65d; break;
                case IconFA.FolderOpen: ret = 0xf07c; break;
                case IconFA.FolderPlus: ret = 0xf65e; break;
                case IconFA.Font: ret = 0xf031; break;
                case IconFA.FontAwesome: ret = 0xf2b4; break;
                case IconFA.FontAwesomeAlt: ret = 0xf35c; break;
                case IconFA.FontAwesomeFlag: ret = 0xf425; break;
                case IconFA.FontAwesomeLogoFull: ret = 0xf4e6; break;
                case IconFA.Fonticons: ret = 0xf280; break;
                case IconFA.FonticonsFi: ret = 0xf3a2; break;
                case IconFA.FootballBall: ret = 0xf44e; break;
                case IconFA.FortAwesome: ret = 0xf286; break;
                case IconFA.FortAwesomeAlt: ret = 0xf3a3; break;
                case IconFA.Forumbee: ret = 0xf211; break;
                case IconFA.Forward: ret = 0xf04e; break;
                case IconFA.Foursquare: ret = 0xf180; break;
                case IconFA.Freebsd: ret = 0xf3a4; break;
                case IconFA.FreeCodeCamp: ret = 0xf2c5; break;
                case IconFA.Frog: ret = 0xf52e; break;
                case IconFA.Frown: ret = 0xf119; break;
                case IconFA.FrownOpen: ret = 0xf57a; break;
                case IconFA.Fulcrum: ret = 0xf50b; break;
                case IconFA.FunnelDollar: ret = 0xf662; break;
                case IconFA.Futbol: ret = 0xf1e3; break;
                case IconFA.GalacticRepublic: ret = 0xf50c; break;
                case IconFA.GalacticSenate: ret = 0xf50d; break;
                case IconFA.Gamepad: ret = 0xf11b; break;
                case IconFA.GasPump: ret = 0xf52f; break;
                case IconFA.Gavel: ret = 0xf0e3; break;
                case IconFA.Gem: ret = 0xf3a5; break;
                case IconFA.Genderless: ret = 0xf22d; break;
                case IconFA.GetPocket: ret = 0xf265; break;
                case IconFA.Gg: ret = 0xf260; break;
                case IconFA.GgCircle: ret = 0xf261; break;
                case IconFA.Ghost: ret = 0xf6e2; break;
                case IconFA.Gift: ret = 0xf06b; break;
                case IconFA.Gifts: ret = 0xf79c; break;
                case IconFA.Git: ret = 0xf1d3; break;
                case IconFA.GitAlt: ret = 0xf841; break;
                case IconFA.Github: ret = 0xf09b; break;
                case IconFA.GithubAlt: ret = 0xf113; break;
                case IconFA.GithubSquare: ret = 0xf092; break;
                case IconFA.Gitkraken: ret = 0xf3a6; break;
                case IconFA.Gitlab: ret = 0xf296; break;
                case IconFA.GitSquare: ret = 0xf1d2; break;
                case IconFA.Gitter: ret = 0xf426; break;
                case IconFA.GlassCheers: ret = 0xf79f; break;
                case IconFA.Glasses: ret = 0xf530; break;
                case IconFA.GlassMartini: ret = 0xf000; break;
                case IconFA.GlassMartiniAlt: ret = 0xf57b; break;
                case IconFA.GlassWhiskey: ret = 0xf7a0; break;
                case IconFA.Glide: ret = 0xf2a5; break;
                case IconFA.GlideG: ret = 0xf2a6; break;
                case IconFA.Globe: ret = 0xf0ac; break;
                case IconFA.GlobeAfrica: ret = 0xf57c; break;
                case IconFA.GlobeAmericas: ret = 0xf57d; break;
                case IconFA.GlobeAsia: ret = 0xf57e; break;
                case IconFA.GlobeEurope: ret = 0xf7a2; break;
                case IconFA.Gofore: ret = 0xf3a7; break;
                case IconFA.GolfBall: ret = 0xf450; break;
                case IconFA.Goodreads: ret = 0xf3a8; break;
                case IconFA.GoodreadsG: ret = 0xf3a9; break;
                case IconFA.Google: ret = 0xf1a0; break;
                case IconFA.GoogleDrive: ret = 0xf3aa; break;
                case IconFA.GooglePay: ret = 0xe079; break;
                case IconFA.GooglePlay: ret = 0xf3ab; break;
                case IconFA.GooglePlus: ret = 0xf2b3; break;
                case IconFA.GooglePlusG: ret = 0xf0d5; break;
                case IconFA.GooglePlusSquare: ret = 0xf0d4; break;
                case IconFA.GoogleWallet: ret = 0xf1ee; break;
                case IconFA.Gopuram: ret = 0xf664; break;
                case IconFA.GraduationCap: ret = 0xf19d; break;
                case IconFA.Gratipay: ret = 0xf184; break;
                case IconFA.Grav: ret = 0xf2d6; break;
                case IconFA.GreaterThan: ret = 0xf531; break;
                case IconFA.GreaterThanEqual: ret = 0xf532; break;
                case IconFA.Grimace: ret = 0xf57f; break;
                case IconFA.Grin: ret = 0xf580; break;
                case IconFA.GrinAlt: ret = 0xf581; break;
                case IconFA.GrinBeam: ret = 0xf582; break;
                case IconFA.GrinBeamSweat: ret = 0xf583; break;
                case IconFA.GrinHearts: ret = 0xf584; break;
                case IconFA.GrinSquint: ret = 0xf585; break;
                case IconFA.GrinSquintTears: ret = 0xf586; break;
                case IconFA.GrinStars: ret = 0xf587; break;
                case IconFA.GrinTears: ret = 0xf588; break;
                case IconFA.GrinTongue: ret = 0xf589; break;
                case IconFA.GrinTongueSquint: ret = 0xf58a; break;
                case IconFA.GrinTongueWink: ret = 0xf58b; break;
                case IconFA.GrinWink: ret = 0xf58c; break;
                case IconFA.Gripfire: ret = 0xf3ac; break;
                case IconFA.GripHorizontal: ret = 0xf58d; break;
                case IconFA.GripLines: ret = 0xf7a4; break;
                case IconFA.GripLinesVertical: ret = 0xf7a5; break;
                case IconFA.GripVertical: ret = 0xf58e; break;
                case IconFA.Grunt: ret = 0xf3ad; break;
                case IconFA.Guilded: ret = 0xe07e; break;
                case IconFA.Guitar: ret = 0xf7a6; break;
                case IconFA.Gulp: ret = 0xf3ae; break;
                case IconFA.HackerNews: ret = 0xf1d4; break;
                case IconFA.HackerNewsSquare: ret = 0xf3af; break;
                case IconFA.Hackerrank: ret = 0xf5f7; break;
                case IconFA.Hamburger: ret = 0xf805; break;
                case IconFA.Hammer: ret = 0xf6e3; break;
                case IconFA.Hamsa: ret = 0xf665; break;
                case IconFA.HandHolding: ret = 0xf4bd; break;
                case IconFA.HandHoldingHeart: ret = 0xf4be; break;
                case IconFA.HandHoldingMedical: ret = 0xe05c; break;
                case IconFA.HandHoldingUsd: ret = 0xf4c0; break;
                case IconFA.HandHoldingWater: ret = 0xf4c1; break;
                case IconFA.HandLizard: ret = 0xf258; break;
                case IconFA.HandMiddleFinger: ret = 0xf806; break;
                case IconFA.HandPaper: ret = 0xf256; break;
                case IconFA.HandPeace: ret = 0xf25b; break;
                case IconFA.HandPointDown: ret = 0xf0a7; break;
                case IconFA.HandPointer: ret = 0xf25a; break;
                case IconFA.HandPointLeft: ret = 0xf0a5; break;
                case IconFA.HandPointRight: ret = 0xf0a4; break;
                case IconFA.HandPointUp: ret = 0xf0a6; break;
                case IconFA.HandRock: ret = 0xf255; break;
                case IconFA.Hands: ret = 0xf4c2; break;
                case IconFA.HandScissors: ret = 0xf257; break;
                case IconFA.Handshake: ret = 0xf2b5; break;
                case IconFA.HandshakeAltSlash: ret = 0xe05f; break;
                case IconFA.HandshakeSlash: ret = 0xe060; break;
                case IconFA.HandsHelping: ret = 0xf4c4; break;
                case IconFA.HandSparkles: ret = 0xe05d; break;
                case IconFA.HandSpock: ret = 0xf259; break;
                case IconFA.HandsWash: ret = 0xe05e; break;
                case IconFA.Hanukiah: ret = 0xf6e6; break;
                case IconFA.HardHat: ret = 0xf807; break;
                case IconFA.Hashtag: ret = 0xf292; break;
                case IconFA.HatCowboy: ret = 0xf8c0; break;
                case IconFA.HatCowboySide: ret = 0xf8c1; break;
                case IconFA.HatWizard: ret = 0xf6e8; break;
                case IconFA.Hdd: ret = 0xf0a0; break;
                case IconFA.Heading: ret = 0xf1dc; break;
                case IconFA.Headphones: ret = 0xf025; break;
                case IconFA.HeadphonesAlt: ret = 0xf58f; break;
                case IconFA.Headset: ret = 0xf590; break;
                case IconFA.HeadSideCough: ret = 0xe061; break;
                case IconFA.HeadSideCoughSlash: ret = 0xe062; break;
                case IconFA.HeadSideMask: ret = 0xe063; break;
                case IconFA.HeadSideVirus: ret = 0xe064; break;
                case IconFA.Heart: ret = 0xf004; break;
                case IconFA.Heartbeat: ret = 0xf21e; break;
                case IconFA.HeartBroken: ret = 0xf7a9; break;
                case IconFA.Helicopter: ret = 0xf533; break;
                case IconFA.Highlighter: ret = 0xf591; break;
                case IconFA.Hiking: ret = 0xf6ec; break;
                case IconFA.Hippo: ret = 0xf6ed; break;
                case IconFA.Hips: ret = 0xf452; break;
                case IconFA.HireAHelper: ret = 0xf3b0; break;
                case IconFA.History: ret = 0xf1da; break;
                case IconFA.Hive: ret = 0xe07f; break;
                case IconFA.HockeyPuck: ret = 0xf453; break;
                case IconFA.HollyBerry: ret = 0xf7aa; break;
                case IconFA.Home: ret = 0xf015; break;
                case IconFA.Hooli: ret = 0xf427; break;
                case IconFA.Hornbill: ret = 0xf592; break;
                case IconFA.Horse: ret = 0xf6f0; break;
                case IconFA.HorseHead: ret = 0xf7ab; break;
                case IconFA.Hospital: ret = 0xf0f8; break;
                case IconFA.HospitalAlt: ret = 0xf47d; break;
                case IconFA.HospitalSymbol: ret = 0xf47e; break;
                case IconFA.HospitalUser: ret = 0xf80d; break;
                case IconFA.Hotdog: ret = 0xf80f; break;
                case IconFA.Hotel: ret = 0xf594; break;
                case IconFA.Hotjar: ret = 0xf3b1; break;
                case IconFA.HotTub: ret = 0xf593; break;
                case IconFA.Hourglass: ret = 0xf254; break;
                case IconFA.HourglassEnd: ret = 0xf253; break;
                case IconFA.HourglassHalf: ret = 0xf252; break;
                case IconFA.HourglassStart: ret = 0xf251; break;
                case IconFA.HouseDamage: ret = 0xf6f1; break;
                case IconFA.HouseUser: ret = 0xe065; break;
                case IconFA.Houzz: ret = 0xf27c; break;
                case IconFA.Hryvnia: ret = 0xf6f2; break;
                case IconFA.HSquare: ret = 0xf0fd; break;
                case IconFA.Html5: ret = 0xf13b; break;
                case IconFA.Hubspot: ret = 0xf3b2; break;
                case IconFA.IceCream: ret = 0xf810; break;
                case IconFA.Icicles: ret = 0xf7ad; break;
                case IconFA.Icons: ret = 0xf86d; break;
                case IconFA.ICursor: ret = 0xf246; break;
                case IconFA.IdBadge: ret = 0xf2c1; break;
                case IconFA.IdCard: ret = 0xf2c2; break;
                case IconFA.IdCardAlt: ret = 0xf47f; break;
                case IconFA.Ideal: ret = 0xe013; break;
                case IconFA.Igloo: ret = 0xf7ae; break;
                case IconFA.Image: ret = 0xf03e; break;
                case IconFA.Images: ret = 0xf302; break;
                case IconFA.Imdb: ret = 0xf2d8; break;
                case IconFA.Inbox: ret = 0xf01c; break;
                case IconFA.Indent: ret = 0xf03c; break;
                case IconFA.Industry: ret = 0xf275; break;
                case IconFA.Infinity: ret = 0xf534; break;
                case IconFA.Info: ret = 0xf129; break;
                case IconFA.InfoCircle: ret = 0xf05a; break;
                case IconFA.Innosoft: ret = 0xe080; break;
                case IconFA.Instagram: ret = 0xf16d; break;
                case IconFA.InstagramSquare: ret = 0xe055; break;
                case IconFA.Instalod: ret = 0xe081; break;
                case IconFA.Intercom: ret = 0xf7af; break;
                case IconFA.InternetExplorer: ret = 0xf26b; break;
                case IconFA.Invision: ret = 0xf7b0; break;
                case IconFA.Ioxhost: ret = 0xf208; break;
                case IconFA.Italic: ret = 0xf033; break;
                case IconFA.ItchIo: ret = 0xf83a; break;
                case IconFA.Itunes: ret = 0xf3b4; break;
                case IconFA.ItunesNote: ret = 0xf3b5; break;
                case IconFA.Java: ret = 0xf4e4; break;
                case IconFA.Jedi: ret = 0xf669; break;
                case IconFA.JediOrder: ret = 0xf50e; break;
                case IconFA.Jenkins: ret = 0xf3b6; break;
                case IconFA.Jira: ret = 0xf7b1; break;
                case IconFA.Joget: ret = 0xf3b7; break;
                case IconFA.Joint: ret = 0xf595; break;
                case IconFA.Joomla: ret = 0xf1aa; break;
                case IconFA.JournalWhills: ret = 0xf66a; break;
                case IconFA.Js: ret = 0xf3b8; break;
                case IconFA.Jsfiddle: ret = 0xf1cc; break;
                case IconFA.JsSquare: ret = 0xf3b9; break;
                case IconFA.Kaaba: ret = 0xf66b; break;
                case IconFA.Kaggle: ret = 0xf5fa; break;
                case IconFA.Key: ret = 0xf084; break;
                case IconFA.Keybase: ret = 0xf4f5; break;
                case IconFA.Keyboard: ret = 0xf11c; break;
                case IconFA.Keycdn: ret = 0xf3ba; break;
                case IconFA.Khanda: ret = 0xf66d; break;
                case IconFA.Kickstarter: ret = 0xf3bb; break;
                case IconFA.KickstarterK: ret = 0xf3bc; break;
                case IconFA.Kiss: ret = 0xf596; break;
                case IconFA.KissBeam: ret = 0xf597; break;
                case IconFA.KissWinkHeart: ret = 0xf598; break;
                case IconFA.KiwiBird: ret = 0xf535; break;
                case IconFA.Korvue: ret = 0xf42f; break;
                case IconFA.Landmark: ret = 0xf66f; break;
                case IconFA.Language: ret = 0xf1ab; break;
                case IconFA.Laptop: ret = 0xf109; break;
                case IconFA.LaptopCode: ret = 0xf5fc; break;
                case IconFA.LaptopHouse: ret = 0xe066; break;
                case IconFA.LaptopMedical: ret = 0xf812; break;
                case IconFA.Laravel: ret = 0xf3bd; break;
                case IconFA.Lastfm: ret = 0xf202; break;
                case IconFA.LastfmSquare: ret = 0xf203; break;
                case IconFA.Laugh: ret = 0xf599; break;
                case IconFA.LaughBeam: ret = 0xf59a; break;
                case IconFA.LaughSquint: ret = 0xf59b; break;
                case IconFA.LaughWink: ret = 0xf59c; break;
                case IconFA.LayerGroup: ret = 0xf5fd; break;
                case IconFA.Leaf: ret = 0xf06c; break;
                case IconFA.Leanpub: ret = 0xf212; break;
                case IconFA.Lemon: ret = 0xf094; break;
                case IconFA.Less: ret = 0xf41d; break;
                case IconFA.LessThan: ret = 0xf536; break;
                case IconFA.LessThanEqual: ret = 0xf537; break;
                case IconFA.LevelDownAlt: ret = 0xf3be; break;
                case IconFA.LevelUpAlt: ret = 0xf3bf; break;
                case IconFA.LifeRing: ret = 0xf1cd; break;
                case IconFA.Lightbulb: ret = 0xf0eb; break;
                case IconFA.Line: ret = 0xf3c0; break;
                case IconFA.Link: ret = 0xf0c1; break;
                case IconFA.Linkedin: ret = 0xf08c; break;
                case IconFA.LinkedinIn: ret = 0xf0e1; break;
                case IconFA.Linode: ret = 0xf2b8; break;
                case IconFA.Linux: ret = 0xf17c; break;
                case IconFA.LiraSign: ret = 0xf195; break;
                case IconFA.List: ret = 0xf03a; break;
                case IconFA.ListAlt: ret = 0xf022; break;
                case IconFA.ListOl: ret = 0xf0cb; break;
                case IconFA.ListUl: ret = 0xf0ca; break;
                case IconFA.LocationArrow: ret = 0xf124; break;
                case IconFA.Lock: ret = 0xf023; break;
                case IconFA.LockOpen: ret = 0xf3c1; break;
                case IconFA.LongArrowAltDown: ret = 0xf309; break;
                case IconFA.LongArrowAltLeft: ret = 0xf30a; break;
                case IconFA.LongArrowAltRight: ret = 0xf30b; break;
                case IconFA.LongArrowAltUp: ret = 0xf30c; break;
                case IconFA.LowVision: ret = 0xf2a8; break;
                case IconFA.LuggageCart: ret = 0xf59d; break;
                case IconFA.Lungs: ret = 0xf604; break;
                case IconFA.LungsVirus: ret = 0xe067; break;
                case IconFA.Lyft: ret = 0xf3c3; break;
                case IconFA.Magento: ret = 0xf3c4; break;
                case IconFA.Magic: ret = 0xf0d0; break;
                case IconFA.Magnet: ret = 0xf076; break;
                case IconFA.MailBulk: ret = 0xf674; break;
                case IconFA.Mailchimp: ret = 0xf59e; break;
                case IconFA.Male: ret = 0xf183; break;
                case IconFA.Mandalorian: ret = 0xf50f; break;
                case IconFA.Map: ret = 0xf279; break;
                case IconFA.MapMarked: ret = 0xf59f; break;
                case IconFA.MapMarkedAlt: ret = 0xf5a0; break;
                case IconFA.MapMarker: ret = 0xf041; break;
                case IconFA.MapMarkerAlt: ret = 0xf3c5; break;
                case IconFA.MapPin: ret = 0xf276; break;
                case IconFA.MapSigns: ret = 0xf277; break;
                case IconFA.Markdown: ret = 0xf60f; break;
                case IconFA.Marker: ret = 0xf5a1; break;
                case IconFA.Mars: ret = 0xf222; break;
                case IconFA.MarsDouble: ret = 0xf227; break;
                case IconFA.MarsStroke: ret = 0xf229; break;
                case IconFA.MarsStrokeH: ret = 0xf22b; break;
                case IconFA.MarsStrokeV: ret = 0xf22a; break;
                case IconFA.Mask: ret = 0xf6fa; break;
                case IconFA.Mastodon: ret = 0xf4f6; break;
                case IconFA.Maxcdn: ret = 0xf136; break;
                case IconFA.Mdb: ret = 0xf8ca; break;
                case IconFA.Medal: ret = 0xf5a2; break;
                case IconFA.Medapps: ret = 0xf3c6; break;
                case IconFA.Medium: ret = 0xf23a; break;
                case IconFA.MediumM: ret = 0xf3c7; break;
                case IconFA.Medkit: ret = 0xf0fa; break;
                case IconFA.Medrt: ret = 0xf3c8; break;
                case IconFA.Meetup: ret = 0xf2e0; break;
                case IconFA.Megaport: ret = 0xf5a3; break;
                case IconFA.Meh: ret = 0xf11a; break;
                case IconFA.MehBlank: ret = 0xf5a4; break;
                case IconFA.MehRollingEyes: ret = 0xf5a5; break;
                case IconFA.Memory: ret = 0xf538; break;
                case IconFA.Mendeley: ret = 0xf7b3; break;
                case IconFA.Menorah: ret = 0xf676; break;
                case IconFA.Mercury: ret = 0xf223; break;
                case IconFA.Meteor: ret = 0xf753; break;
                case IconFA.Microblog: ret = 0xe01a; break;
                case IconFA.Microchip: ret = 0xf2db; break;
                case IconFA.Microphone: ret = 0xf130; break;
                case IconFA.MicrophoneAlt: ret = 0xf3c9; break;
                case IconFA.MicrophoneAltSlash: ret = 0xf539; break;
                case IconFA.MicrophoneSlash: ret = 0xf131; break;
                case IconFA.Microscope: ret = 0xf610; break;
                case IconFA.Microsoft: ret = 0xf3ca; break;
                case IconFA.Minus: ret = 0xf068; break;
                case IconFA.MinusCircle: ret = 0xf056; break;
                case IconFA.MinusSquare: ret = 0xf146; break;
                case IconFA.Mitten: ret = 0xf7b5; break;
                case IconFA.Mix: ret = 0xf3cb; break;
                case IconFA.Mixcloud: ret = 0xf289; break;
                case IconFA.Mixer: ret = 0xe056; break;
                case IconFA.Mizuni: ret = 0xf3cc; break;
                case IconFA.Mobile: ret = 0xf10b; break;
                case IconFA.MobileAlt: ret = 0xf3cd; break;
                case IconFA.Modx: ret = 0xf285; break;
                case IconFA.Monero: ret = 0xf3d0; break;
                case IconFA.MoneyBill: ret = 0xf0d6; break;
                case IconFA.MoneyBillAlt: ret = 0xf3d1; break;
                case IconFA.MoneyBillWave: ret = 0xf53a; break;
                case IconFA.MoneyBillWaveAlt: ret = 0xf53b; break;
                case IconFA.MoneyCheck: ret = 0xf53c; break;
                case IconFA.MoneyCheckAlt: ret = 0xf53d; break;
                case IconFA.Monument: ret = 0xf5a6; break;
                case IconFA.Moon: ret = 0xf186; break;
                case IconFA.MortarPestle: ret = 0xf5a7; break;
                case IconFA.Mosque: ret = 0xf678; break;
                case IconFA.Motorcycle: ret = 0xf21c; break;
                case IconFA.Mountain: ret = 0xf6fc; break;
                case IconFA.Mouse: ret = 0xf8cc; break;
                case IconFA.MousePointer: ret = 0xf245; break;
                case IconFA.MugHot: ret = 0xf7b6; break;
                case IconFA.Music: ret = 0xf001; break;
                case IconFA.Napster: ret = 0xf3d2; break;
                case IconFA.Neos: ret = 0xf612; break;
                case IconFA.NetworkWired: ret = 0xf6ff; break;
                case IconFA.Neuter: ret = 0xf22c; break;
                case IconFA.Newspaper: ret = 0xf1ea; break;
                case IconFA.Nimblr: ret = 0xf5a8; break;
                case IconFA.Node: ret = 0xf419; break;
                case IconFA.NodeJs: ret = 0xf3d3; break;
                case IconFA.NotEqual: ret = 0xf53e; break;
                case IconFA.NotesMedical: ret = 0xf481; break;
                case IconFA.Npm: ret = 0xf3d4; break;
                case IconFA.Ns8: ret = 0xf3d5; break;
                case IconFA.Nutritionix: ret = 0xf3d6; break;
                case IconFA.ObjectGroup: ret = 0xf247; break;
                case IconFA.ObjectUngroup: ret = 0xf248; break;
                case IconFA.OctopusDeploy: ret = 0xe082; break;
                case IconFA.Odnoklassniki: ret = 0xf263; break;
                case IconFA.OdnoklassnikiSquare: ret = 0xf264; break;
                case IconFA.OilCan: ret = 0xf613; break;
                case IconFA.OldRepublic: ret = 0xf510; break;
                case IconFA.Om: ret = 0xf679; break;
                case IconFA.Opencart: ret = 0xf23d; break;
                case IconFA.Openid: ret = 0xf19b; break;
                case IconFA.Opera: ret = 0xf26a; break;
                case IconFA.OptinMonster: ret = 0xf23c; break;
                case IconFA.Orcid: ret = 0xf8d2; break;
                case IconFA.Osi: ret = 0xf41a; break;
                case IconFA.Otter: ret = 0xf700; break;
                case IconFA.Outdent: ret = 0xf03b; break;
                case IconFA.Page4: ret = 0xf3d7; break;
                case IconFA.Pagelines: ret = 0xf18c; break;
                case IconFA.Pager: ret = 0xf815; break;
                case IconFA.PaintBrush: ret = 0xf1fc; break;
                case IconFA.PaintRoller: ret = 0xf5aa; break;
                case IconFA.Palette: ret = 0xf53f; break;
                case IconFA.Palfed: ret = 0xf3d8; break;
                case IconFA.Pallet: ret = 0xf482; break;
                case IconFA.Paperclip: ret = 0xf0c6; break;
                case IconFA.PaperPlane: ret = 0xf1d8; break;
                case IconFA.ParachuteBox: ret = 0xf4cd; break;
                case IconFA.Paragraph: ret = 0xf1dd; break;
                case IconFA.Parking: ret = 0xf540; break;
                case IconFA.Passport: ret = 0xf5ab; break;
                case IconFA.Pastafarianism: ret = 0xf67b; break;
                case IconFA.Paste: ret = 0xf0ea; break;
                case IconFA.Patreon: ret = 0xf3d9; break;
                case IconFA.Pause: ret = 0xf04c; break;
                case IconFA.PauseCircle: ret = 0xf28b; break;
                case IconFA.Paw: ret = 0xf1b0; break;
                case IconFA.Paypal: ret = 0xf1ed; break;
                case IconFA.Peace: ret = 0xf67c; break;
                case IconFA.Pen: ret = 0xf304; break;
                case IconFA.PenAlt: ret = 0xf305; break;
                case IconFA.PencilAlt: ret = 0xf303; break;
                case IconFA.PencilRuler: ret = 0xf5ae; break;
                case IconFA.PenFancy: ret = 0xf5ac; break;
                case IconFA.PenNib: ret = 0xf5ad; break;
                case IconFA.PennyArcade: ret = 0xf704; break;
                case IconFA.PenSquare: ret = 0xf14b; break;
                case IconFA.PeopleArrows: ret = 0xe068; break;
                case IconFA.PeopleCarry: ret = 0xf4ce; break;
                case IconFA.PepperHot: ret = 0xf816; break;
                case IconFA.Perbyte: ret = 0xe083; break;
                case IconFA.Percent: ret = 0xf295; break;
                case IconFA.Percentage: ret = 0xf541; break;
                case IconFA.Periscope: ret = 0xf3da; break;
                case IconFA.PersonBooth: ret = 0xf756; break;
                case IconFA.Phabricator: ret = 0xf3db; break;
                case IconFA.PhoenixFramework: ret = 0xf3dc; break;
                case IconFA.PhoenixSquadron: ret = 0xf511; break;
                case IconFA.Phone: ret = 0xf095; break;
                case IconFA.PhoneAlt: ret = 0xf879; break;
                case IconFA.PhoneSlash: ret = 0xf3dd; break;
                case IconFA.PhoneSquare: ret = 0xf098; break;
                case IconFA.PhoneSquareAlt: ret = 0xf87b; break;
                case IconFA.PhoneVolume: ret = 0xf2a0; break;
                case IconFA.PhotoVideo: ret = 0xf87c; break;
                case IconFA.Php: ret = 0xf457; break;
                case IconFA.PiedPiper: ret = 0xf2ae; break;
                case IconFA.PiedPiperAlt: ret = 0xf1a8; break;
                case IconFA.PiedPiperHat: ret = 0xf4e5; break;
                case IconFA.PiedPiperPp: ret = 0xf1a7; break;
                case IconFA.PiedPiperSquare: ret = 0xe01e; break;
                case IconFA.PiggyBank: ret = 0xf4d3; break;
                case IconFA.Pills: ret = 0xf484; break;
                case IconFA.Pinterest: ret = 0xf0d2; break;
                case IconFA.PinterestP: ret = 0xf231; break;
                case IconFA.PinterestSquare: ret = 0xf0d3; break;
                case IconFA.PizzaSlice: ret = 0xf818; break;
                case IconFA.PlaceOfWorship: ret = 0xf67f; break;
                case IconFA.Plane: ret = 0xf072; break;
                case IconFA.PlaneArrival: ret = 0xf5af; break;
                case IconFA.PlaneDeparture: ret = 0xf5b0; break;
                case IconFA.PlaneSlash: ret = 0xe069; break;
                case IconFA.Play: ret = 0xf04b; break;
                case IconFA.PlayCircle: ret = 0xf144; break;
                case IconFA.Playstation: ret = 0xf3df; break;
                case IconFA.Plug: ret = 0xf1e6; break;
                case IconFA.Plus: ret = 0xf067; break;
                case IconFA.PlusCircle: ret = 0xf055; break;
                case IconFA.PlusSquare: ret = 0xf0fe; break;
                case IconFA.Podcast: ret = 0xf2ce; break;
                case IconFA.Poll: ret = 0xf681; break;
                case IconFA.PollH: ret = 0xf682; break;
                case IconFA.Poo: ret = 0xf2fe; break;
                case IconFA.Poop: ret = 0xf619; break;
                case IconFA.PooStorm: ret = 0xf75a; break;
                case IconFA.Portrait: ret = 0xf3e0; break;
                case IconFA.PoundSign: ret = 0xf154; break;
                case IconFA.PowerOff: ret = 0xf011; break;
                case IconFA.Pray: ret = 0xf683; break;
                case IconFA.PrayingHands: ret = 0xf684; break;
                case IconFA.Prescription: ret = 0xf5b1; break;
                case IconFA.PrescriptionBottle: ret = 0xf485; break;
                case IconFA.PrescriptionBottleAlt: ret = 0xf486; break;
                case IconFA.Print: ret = 0xf02f; break;
                case IconFA.Procedures: ret = 0xf487; break;
                case IconFA.ProductHunt: ret = 0xf288; break;
                case IconFA.ProjectDiagram: ret = 0xf542; break;
                case IconFA.PumpMedical: ret = 0xe06a; break;
                case IconFA.PumpSoap: ret = 0xe06b; break;
                case IconFA.Pushed: ret = 0xf3e1; break;
                case IconFA.PuzzlePiece: ret = 0xf12e; break;
                case IconFA.Python: ret = 0xf3e2; break;
                case IconFA.Qq: ret = 0xf1d6; break;
                case IconFA.Qrcode: ret = 0xf029; break;
                case IconFA.Question: ret = 0xf128; break;
                case IconFA.QuestionCircle: ret = 0xf059; break;
                case IconFA.Quidditch: ret = 0xf458; break;
                case IconFA.Quinscape: ret = 0xf459; break;
                case IconFA.Quora: ret = 0xf2c4; break;
                case IconFA.QuoteLeft: ret = 0xf10d; break;
                case IconFA.QuoteRight: ret = 0xf10e; break;
                case IconFA.Quran: ret = 0xf687; break;
                case IconFA.Radiation: ret = 0xf7b9; break;
                case IconFA.RadiationAlt: ret = 0xf7ba; break;
                case IconFA.Rainbow: ret = 0xf75b; break;
                case IconFA.Random: ret = 0xf074; break;
                case IconFA.RaspberryPi: ret = 0xf7bb; break;
                case IconFA.Ravelry: ret = 0xf2d9; break;
                case IconFA.React: ret = 0xf41b; break;
                case IconFA.Reacteurope: ret = 0xf75d; break;
                case IconFA.Readme: ret = 0xf4d5; break;
                case IconFA.Rebel: ret = 0xf1d0; break;
                case IconFA.Receipt: ret = 0xf543; break;
                case IconFA.RecordVinyl: ret = 0xf8d9; break;
                case IconFA.Recycle: ret = 0xf1b8; break;
                case IconFA.Reddit: ret = 0xf1a1; break;
                case IconFA.RedditAlien: ret = 0xf281; break;
                case IconFA.RedditSquare: ret = 0xf1a2; break;
                case IconFA.Redhat: ret = 0xf7bc; break;
                case IconFA.Redo: ret = 0xf01e; break;
                case IconFA.RedoAlt: ret = 0xf2f9; break;
                case IconFA.RedRiver: ret = 0xf3e3; break;
                case IconFA.Registered: ret = 0xf25d; break;
                case IconFA.RemoveFormat: ret = 0xf87d; break;
                case IconFA.Renren: ret = 0xf18b; break;
                case IconFA.Reply: ret = 0xf3e5; break;
                case IconFA.ReplyAll: ret = 0xf122; break;
                case IconFA.Replyd: ret = 0xf3e6; break;
                case IconFA.Republican: ret = 0xf75e; break;
                case IconFA.Researchgate: ret = 0xf4f8; break;
                case IconFA.Resolving: ret = 0xf3e7; break;
                case IconFA.Restroom: ret = 0xf7bd; break;
                case IconFA.Retweet: ret = 0xf079; break;
                case IconFA.Rev: ret = 0xf5b2; break;
                case IconFA.Ribbon: ret = 0xf4d6; break;
                case IconFA.Ring: ret = 0xf70b; break;
                case IconFA.Road: ret = 0xf018; break;
                case IconFA.Robot: ret = 0xf544; break;
                case IconFA.Rocket: ret = 0xf135; break;
                case IconFA.Rocketchat: ret = 0xf3e8; break;
                case IconFA.Rockrms: ret = 0xf3e9; break;
                case IconFA.Route: ret = 0xf4d7; break;
                case IconFA.RProject: ret = 0xf4f7; break;
                case IconFA.Rss: ret = 0xf09e; break;
                case IconFA.RssSquare: ret = 0xf143; break;
                case IconFA.RubleSign: ret = 0xf158; break;
                case IconFA.Ruler: ret = 0xf545; break;
                case IconFA.RulerCombined: ret = 0xf546; break;
                case IconFA.RulerHorizontal: ret = 0xf547; break;
                case IconFA.RulerVertical: ret = 0xf548; break;
                case IconFA.Running: ret = 0xf70c; break;
                case IconFA.RupeeSign: ret = 0xf156; break;
                case IconFA.Rust: ret = 0xe07a; break;
                case IconFA.SadCry: ret = 0xf5b3; break;
                case IconFA.SadTear: ret = 0xf5b4; break;
                case IconFA.Safari: ret = 0xf267; break;
                case IconFA.Salesforce: ret = 0xf83b; break;
                case IconFA.Sass: ret = 0xf41e; break;
                case IconFA.Satellite: ret = 0xf7bf; break;
                case IconFA.SatelliteDish: ret = 0xf7c0; break;
                case IconFA.Save: ret = 0xf0c7; break;
                case IconFA.Schlix: ret = 0xf3ea; break;
                case IconFA.School: ret = 0xf549; break;
                case IconFA.Screwdriver: ret = 0xf54a; break;
                case IconFA.Scribd: ret = 0xf28a; break;
                case IconFA.Scroll: ret = 0xf70e; break;
                case IconFA.SdCard: ret = 0xf7c2; break;
                case IconFA.Search: ret = 0xf002; break;
                case IconFA.SearchDollar: ret = 0xf688; break;
                case IconFA.Searchengin: ret = 0xf3eb; break;
                case IconFA.SearchLocation: ret = 0xf689; break;
                case IconFA.SearchMinus: ret = 0xf010; break;
                case IconFA.SearchPlus: ret = 0xf00e; break;
                case IconFA.Seedling: ret = 0xf4d8; break;
                case IconFA.Sellcast: ret = 0xf2da; break;
                case IconFA.Sellsy: ret = 0xf213; break;
                case IconFA.Server: ret = 0xf233; break;
                case IconFA.Servicestack: ret = 0xf3ec; break;
                case IconFA.Shapes: ret = 0xf61f; break;
                case IconFA.Share: ret = 0xf064; break;
                case IconFA.ShareAlt: ret = 0xf1e0; break;
                case IconFA.ShareAltSquare: ret = 0xf1e1; break;
                case IconFA.ShareSquare: ret = 0xf14d; break;
                case IconFA.ShekelSign: ret = 0xf20b; break;
                case IconFA.ShieldAlt: ret = 0xf3ed; break;
                case IconFA.ShieldVirus: ret = 0xe06c; break;
                case IconFA.Ship: ret = 0xf21a; break;
                case IconFA.ShippingFast: ret = 0xf48b; break;
                case IconFA.Shirtsinbulk: ret = 0xf214; break;
                case IconFA.ShoePrints: ret = 0xf54b; break;
                case IconFA.Shopify: ret = 0xe057; break;
                case IconFA.ShoppingBag: ret = 0xf290; break;
                case IconFA.ShoppingBasket: ret = 0xf291; break;
                case IconFA.ShoppingCart: ret = 0xf07a; break;
                case IconFA.Shopware: ret = 0xf5b5; break;
                case IconFA.Shower: ret = 0xf2cc; break;
                case IconFA.ShuttleVan: ret = 0xf5b6; break;
                case IconFA.Sign: ret = 0xf4d9; break;
                case IconFA.Signal: ret = 0xf012; break;
                case IconFA.Signature: ret = 0xf5b7; break;
                case IconFA.SignInAlt: ret = 0xf2f6; break;
                case IconFA.SignLanguage: ret = 0xf2a7; break;
                case IconFA.SignOutAlt: ret = 0xf2f5; break;
                case IconFA.SimCard: ret = 0xf7c4; break;
                case IconFA.Simplybuilt: ret = 0xf215; break;
                case IconFA.Sink: ret = 0xe06d; break;
                case IconFA.Sistrix: ret = 0xf3ee; break;
                case IconFA.Sitemap: ret = 0xf0e8; break;
                case IconFA.Sith: ret = 0xf512; break;
                case IconFA.Skating: ret = 0xf7c5; break;
                case IconFA.Sketch: ret = 0xf7c6; break;
                case IconFA.Skiing: ret = 0xf7c9; break;
                case IconFA.SkiingNordic: ret = 0xf7ca; break;
                case IconFA.Skull: ret = 0xf54c; break;
                case IconFA.SkullCrossbones: ret = 0xf714; break;
                case IconFA.Skyatlas: ret = 0xf216; break;
                case IconFA.Skype: ret = 0xf17e; break;
                case IconFA.Slack: ret = 0xf198; break;
                case IconFA.SlackHash: ret = 0xf3ef; break;
                case IconFA.Slash: ret = 0xf715; break;
                case IconFA.Sleigh: ret = 0xf7cc; break;
                case IconFA.SlidersH: ret = 0xf1de; break;
                case IconFA.Slideshare: ret = 0xf1e7; break;
                case IconFA.Smile: ret = 0xf118; break;
                case IconFA.SmileBeam: ret = 0xf5b8; break;
                case IconFA.SmileWink: ret = 0xf4da; break;
                case IconFA.Smog: ret = 0xf75f; break;
                case IconFA.Smoking: ret = 0xf48d; break;
                case IconFA.SmokingBan: ret = 0xf54d; break;
                case IconFA.Sms: ret = 0xf7cd; break;
                case IconFA.Snapchat: ret = 0xf2ab; break;
                case IconFA.SnapchatGhost: ret = 0xf2ac; break;
                case IconFA.SnapchatSquare: ret = 0xf2ad; break;
                case IconFA.Snowboarding: ret = 0xf7ce; break;
                case IconFA.Snowflake: ret = 0xf2dc; break;
                case IconFA.Snowman: ret = 0xf7d0; break;
                case IconFA.Snowplow: ret = 0xf7d2; break;
                case IconFA.Soap: ret = 0xe06e; break;
                case IconFA.Socks: ret = 0xf696; break;
                case IconFA.SolarPanel: ret = 0xf5ba; break;
                case IconFA.Sort: ret = 0xf0dc; break;
                case IconFA.SortAlphaDown: ret = 0xf15d; break;
                case IconFA.SortAlphaDownAlt: ret = 0xf881; break;
                case IconFA.SortAlphaUp: ret = 0xf15e; break;
                case IconFA.SortAlphaUpAlt: ret = 0xf882; break;
                case IconFA.SortAmountDown: ret = 0xf160; break;
                case IconFA.SortAmountDownAlt: ret = 0xf884; break;
                case IconFA.SortAmountUp: ret = 0xf161; break;
                case IconFA.SortAmountUpAlt: ret = 0xf885; break;
                case IconFA.SortDown: ret = 0xf0dd; break;
                case IconFA.SortNumericDown: ret = 0xf162; break;
                case IconFA.SortNumericDownAlt: ret = 0xf886; break;
                case IconFA.SortNumericUp: ret = 0xf163; break;
                case IconFA.SortNumericUpAlt: ret = 0xf887; break;
                case IconFA.SortUp: ret = 0xf0de; break;
                case IconFA.Soundcloud: ret = 0xf1be; break;
                case IconFA.Sourcetree: ret = 0xf7d3; break;
                case IconFA.Spa: ret = 0xf5bb; break;
                case IconFA.SpaceShuttle: ret = 0xf197; break;
                case IconFA.Speakap: ret = 0xf3f3; break;
                case IconFA.SpeakerDeck: ret = 0xf83c; break;
                case IconFA.SpellCheck: ret = 0xf891; break;
                case IconFA.Spider: ret = 0xf717; break;
                case IconFA.Spinner: ret = 0xf110; break;
                case IconFA.Splotch: ret = 0xf5bc; break;
                case IconFA.Spotify: ret = 0xf1bc; break;
                case IconFA.SprayCan: ret = 0xf5bd; break;
                case IconFA.Square: ret = 0xf0c8; break;
                case IconFA.SquareFull: ret = 0xf45c; break;
                case IconFA.SquareRootAlt: ret = 0xf698; break;
                case IconFA.Squarespace: ret = 0xf5be; break;
                case IconFA.StackExchange: ret = 0xf18d; break;
                case IconFA.StackOverflow: ret = 0xf16c; break;
                case IconFA.Stackpath: ret = 0xf842; break;
                case IconFA.Stamp: ret = 0xf5bf; break;
                case IconFA.Star: ret = 0xf005; break;
                case IconFA.StarAndCrescent: ret = 0xf699; break;
                case IconFA.StarHalf: ret = 0xf089; break;
                case IconFA.StarHalfAlt: ret = 0xf5c0; break;
                case IconFA.StarOfDavid: ret = 0xf69a; break;
                case IconFA.StarOfLife: ret = 0xf621; break;
                case IconFA.Staylinked: ret = 0xf3f5; break;
                case IconFA.Steam: ret = 0xf1b6; break;
                case IconFA.SteamSquare: ret = 0xf1b7; break;
                case IconFA.SteamSymbol: ret = 0xf3f6; break;
                case IconFA.StepBackward: ret = 0xf048; break;
                case IconFA.StepForward: ret = 0xf051; break;
                case IconFA.Stethoscope: ret = 0xf0f1; break;
                case IconFA.StickerMule: ret = 0xf3f7; break;
                case IconFA.StickyNote: ret = 0xf249; break;
                case IconFA.Stop: ret = 0xf04d; break;
                case IconFA.StopCircle: ret = 0xf28d; break;
                case IconFA.Stopwatch: ret = 0xf2f2; break;
                case IconFA.Stopwatch20: ret = 0xe06f; break;
                case IconFA.Store: ret = 0xf54e; break;
                case IconFA.StoreAlt: ret = 0xf54f; break;
                case IconFA.StoreAltSlash: ret = 0xe070; break;
                case IconFA.StoreSlash: ret = 0xe071; break;
                case IconFA.Strava: ret = 0xf428; break;
                case IconFA.Stream: ret = 0xf550; break;
                case IconFA.StreetView: ret = 0xf21d; break;
                case IconFA.Strikethrough: ret = 0xf0cc; break;
                case IconFA.Stripe: ret = 0xf429; break;
                case IconFA.StripeS: ret = 0xf42a; break;
                case IconFA.Stroopwafel: ret = 0xf551; break;
                case IconFA.Studiovinari: ret = 0xf3f8; break;
                case IconFA.Stumbleupon: ret = 0xf1a4; break;
                case IconFA.StumbleuponCircle: ret = 0xf1a3; break;
                case IconFA.Subscript: ret = 0xf12c; break;
                case IconFA.Subway: ret = 0xf239; break;
                case IconFA.Suitcase: ret = 0xf0f2; break;
                case IconFA.SuitcaseRolling: ret = 0xf5c1; break;
                case IconFA.Sun: ret = 0xf185; break;
                case IconFA.Superpowers: ret = 0xf2dd; break;
                case IconFA.Superscript: ret = 0xf12b; break;
                case IconFA.Supple: ret = 0xf3f9; break;
                case IconFA.Surprise: ret = 0xf5c2; break;
                case IconFA.Suse: ret = 0xf7d6; break;
                case IconFA.Swatchbook: ret = 0xf5c3; break;
                case IconFA.Swift: ret = 0xf8e1; break;
                case IconFA.Swimmer: ret = 0xf5c4; break;
                case IconFA.SwimmingPool: ret = 0xf5c5; break;
                case IconFA.Symfony: ret = 0xf83d; break;
                case IconFA.Synagogue: ret = 0xf69b; break;
                case IconFA.Sync: ret = 0xf021; break;
                case IconFA.SyncAlt: ret = 0xf2f1; break;
                case IconFA.Syringe: ret = 0xf48e; break;
                case IconFA.Table: ret = 0xf0ce; break;
                case IconFA.Tablet: ret = 0xf10a; break;
                case IconFA.TabletAlt: ret = 0xf3fa; break;
                case IconFA.TableTennis: ret = 0xf45d; break;
                case IconFA.Tablets: ret = 0xf490; break;
                case IconFA.TachometerAlt: ret = 0xf3fd; break;
                case IconFA.Tag: ret = 0xf02b; break;
                case IconFA.Tags: ret = 0xf02c; break;
                case IconFA.Tape: ret = 0xf4db; break;
                case IconFA.Tasks: ret = 0xf0ae; break;
                case IconFA.Taxi: ret = 0xf1ba; break;
                case IconFA.Teamspeak: ret = 0xf4f9; break;
                case IconFA.Teeth: ret = 0xf62e; break;
                case IconFA.TeethOpen: ret = 0xf62f; break;
                case IconFA.Telegram: ret = 0xf2c6; break;
                case IconFA.TelegramPlane: ret = 0xf3fe; break;
                case IconFA.TemperatureHigh: ret = 0xf769; break;
                case IconFA.TemperatureLow: ret = 0xf76b; break;
                case IconFA.TencentWeibo: ret = 0xf1d5; break;
                case IconFA.Tenge: ret = 0xf7d7; break;
                case IconFA.Terminal: ret = 0xf120; break;
                case IconFA.TextHeight: ret = 0xf034; break;
                case IconFA.TextWidth: ret = 0xf035; break;
                case IconFA.Th: ret = 0xf00a; break;
                case IconFA.TheaterMasks: ret = 0xf630; break;
                case IconFA.Themeco: ret = 0xf5c6; break;
                case IconFA.Themeisle: ret = 0xf2b2; break;
                case IconFA.TheRedYeti: ret = 0xf69d; break;
                case IconFA.Thermometer: ret = 0xf491; break;
                case IconFA.ThermometerEmpty: ret = 0xf2cb; break;
                case IconFA.ThermometerFull: ret = 0xf2c7; break;
                case IconFA.ThermometerHalf: ret = 0xf2c9; break;
                case IconFA.ThermometerQuarter: ret = 0xf2ca; break;
                case IconFA.ThermometerThreeQuarters: ret = 0xf2c8; break;
                case IconFA.ThinkPeaks: ret = 0xf731; break;
                case IconFA.ThLarge: ret = 0xf009; break;
                case IconFA.ThList: ret = 0xf00b; break;
                case IconFA.ThumbsDown: ret = 0xf165; break;
                case IconFA.ThumbsUp: ret = 0xf164; break;
                case IconFA.Thumbtack: ret = 0xf08d; break;
                case IconFA.TicketAlt: ret = 0xf3ff; break;
                case IconFA.Tiktok: ret = 0xe07b; break;
                case IconFA.Times: ret = 0xf00d; break;
                case IconFA.TimesCircle: ret = 0xf057; break;
                case IconFA.Tint: ret = 0xf043; break;
                case IconFA.TintSlash: ret = 0xf5c7; break;
                case IconFA.Tired: ret = 0xf5c8; break;
                case IconFA.ToggleOff: ret = 0xf204; break;
                case IconFA.ToggleOn: ret = 0xf205; break;
                case IconFA.Toilet: ret = 0xf7d8; break;
                case IconFA.ToiletPaper: ret = 0xf71e; break;
                case IconFA.ToiletPaperSlash: ret = 0xe072; break;
                case IconFA.Toolbox: ret = 0xf552; break;
                case IconFA.Tools: ret = 0xf7d9; break;
                case IconFA.Tooth: ret = 0xf5c9; break;
                case IconFA.Torah: ret = 0xf6a0; break;
                case IconFA.ToriiGate: ret = 0xf6a1; break;
                case IconFA.Tractor: ret = 0xf722; break;
                case IconFA.TradeFederation: ret = 0xf513; break;
                case IconFA.Trademark: ret = 0xf25c; break;
                case IconFA.TrafficLight: ret = 0xf637; break;
                case IconFA.Trailer: ret = 0xe041; break;
                case IconFA.Train: ret = 0xf238; break;
                case IconFA.Tram: ret = 0xf7da; break;
                case IconFA.Transgender: ret = 0xf224; break;
                case IconFA.TransgenderAlt: ret = 0xf225; break;
                case IconFA.Trash: ret = 0xf1f8; break;
                case IconFA.TrashAlt: ret = 0xf2ed; break;
                case IconFA.TrashRestore: ret = 0xf829; break;
                case IconFA.TrashRestoreAlt: ret = 0xf82a; break;
                case IconFA.Tree: ret = 0xf1bb; break;
                case IconFA.Trello: ret = 0xf181; break;
                case IconFA.Tripadvisor: ret = 0xf262; break;
                case IconFA.Trophy: ret = 0xf091; break;
                case IconFA.Truck: ret = 0xf0d1; break;
                case IconFA.TruckLoading: ret = 0xf4de; break;
                case IconFA.TruckMonster: ret = 0xf63b; break;
                case IconFA.TruckMoving: ret = 0xf4df; break;
                case IconFA.TruckPickup: ret = 0xf63c; break;
                case IconFA.Tshirt: ret = 0xf553; break;
                case IconFA.Tty: ret = 0xf1e4; break;
                case IconFA.Tumblr: ret = 0xf173; break;
                case IconFA.TumblrSquare: ret = 0xf174; break;
                case IconFA.Tv: ret = 0xf26c; break;
                case IconFA.Twitch: ret = 0xf1e8; break;
                case IconFA.Twitter: ret = 0xf099; break;
                case IconFA.TwitterSquare: ret = 0xf081; break;
                case IconFA.Typo3: ret = 0xf42b; break;
                case IconFA.Uber: ret = 0xf402; break;
                case IconFA.Ubuntu: ret = 0xf7df; break;
                case IconFA.Uikit: ret = 0xf403; break;
                case IconFA.Umbraco: ret = 0xf8e8; break;
                case IconFA.Umbrella: ret = 0xf0e9; break;
                case IconFA.UmbrellaBeach: ret = 0xf5ca; break;
                case IconFA.Uncharted: ret = 0xe084; break;
                case IconFA.Underline: ret = 0xf0cd; break;
                case IconFA.Undo: ret = 0xf0e2; break;
                case IconFA.UndoAlt: ret = 0xf2ea; break;
                case IconFA.Uniregistry: ret = 0xf404; break;
                case IconFA.Unity: ret = 0xe049; break;
                case IconFA.UniversalAccess: ret = 0xf29a; break;
                case IconFA.University: ret = 0xf19c; break;
                case IconFA.Unlink: ret = 0xf127; break;
                case IconFA.Unlock: ret = 0xf09c; break;
                case IconFA.UnlockAlt: ret = 0xf13e; break;
                case IconFA.Unsplash: ret = 0xe07c; break;
                case IconFA.Untappd: ret = 0xf405; break;
                case IconFA.Upload: ret = 0xf093; break;
                case IconFA.Ups: ret = 0xf7e0; break;
                case IconFA.Usb: ret = 0xf287; break;
                case IconFA.User: ret = 0xf007; break;
                case IconFA.UserAlt: ret = 0xf406; break;
                case IconFA.UserAltSlash: ret = 0xf4fa; break;
                case IconFA.UserAstronaut: ret = 0xf4fb; break;
                case IconFA.UserCheck: ret = 0xf4fc; break;
                case IconFA.UserCircle: ret = 0xf2bd; break;
                case IconFA.UserClock: ret = 0xf4fd; break;
                case IconFA.UserCog: ret = 0xf4fe; break;
                case IconFA.UserEdit: ret = 0xf4ff; break;
                case IconFA.UserFriends: ret = 0xf500; break;
                case IconFA.UserGraduate: ret = 0xf501; break;
                case IconFA.UserInjured: ret = 0xf728; break;
                case IconFA.UserLock: ret = 0xf502; break;
                case IconFA.UserMd: ret = 0xf0f0; break;
                case IconFA.UserMinus: ret = 0xf503; break;
                case IconFA.UserNinja: ret = 0xf504; break;
                case IconFA.UserNurse: ret = 0xf82f; break;
                case IconFA.UserPlus: ret = 0xf234; break;
                case IconFA.Users: ret = 0xf0c0; break;
                case IconFA.UsersCog: ret = 0xf509; break;
                case IconFA.UserSecret: ret = 0xf21b; break;
                case IconFA.UserShield: ret = 0xf505; break;
                case IconFA.UserSlash: ret = 0xf506; break;
                case IconFA.UsersSlash: ret = 0xe073; break;
                case IconFA.UserTag: ret = 0xf507; break;
                case IconFA.UserTie: ret = 0xf508; break;
                case IconFA.UserTimes: ret = 0xf235; break;
                case IconFA.Usps: ret = 0xf7e1; break;
                case IconFA.Ussunnah: ret = 0xf407; break;
                case IconFA.Utensils: ret = 0xf2e7; break;
                case IconFA.UtensilSpoon: ret = 0xf2e5; break;
                case IconFA.Vaadin: ret = 0xf408; break;
                case IconFA.VectorSquare: ret = 0xf5cb; break;
                case IconFA.Venus: ret = 0xf221; break;
                case IconFA.VenusDouble: ret = 0xf226; break;
                case IconFA.VenusMars: ret = 0xf228; break;
                case IconFA.Vest: ret = 0xe085; break;
                case IconFA.VestPatches: ret = 0xe086; break;
                case IconFA.Viacoin: ret = 0xf237; break;
                case IconFA.Viadeo: ret = 0xf2a9; break;
                case IconFA.ViadeoSquare: ret = 0xf2aa; break;
                case IconFA.Vial: ret = 0xf492; break;
                case IconFA.Vials: ret = 0xf493; break;
                case IconFA.Viber: ret = 0xf409; break;
                case IconFA.Video: ret = 0xf03d; break;
                case IconFA.VideoSlash: ret = 0xf4e2; break;
                case IconFA.Vihara: ret = 0xf6a7; break;
                case IconFA.Vimeo: ret = 0xf40a; break;
                case IconFA.VimeoSquare: ret = 0xf194; break;
                case IconFA.VimeoV: ret = 0xf27d; break;
                case IconFA.Vine: ret = 0xf1ca; break;
                case IconFA.Virus: ret = 0xe074; break;
                case IconFA.Viruses: ret = 0xe076; break;
                case IconFA.VirusSlash: ret = 0xe075; break;
                case IconFA.Vk: ret = 0xf189; break;
                case IconFA.Vnv: ret = 0xf40b; break;
                case IconFA.Voicemail: ret = 0xf897; break;
                case IconFA.VolleyballBall: ret = 0xf45f; break;
                case IconFA.VolumeDown: ret = 0xf027; break;
                case IconFA.VolumeMute: ret = 0xf6a9; break;
                case IconFA.VolumeOff: ret = 0xf026; break;
                case IconFA.VolumeUp: ret = 0xf028; break;
                case IconFA.VoteYea: ret = 0xf772; break;
                case IconFA.VrCardboard: ret = 0xf729; break;
                case IconFA.Vuejs: ret = 0xf41f; break;
                case IconFA.Walking: ret = 0xf554; break;
                case IconFA.Wallet: ret = 0xf555; break;
                case IconFA.Warehouse: ret = 0xf494; break;
                case IconFA.WatchmanMonitoring: ret = 0xe087; break;
                case IconFA.Water: ret = 0xf773; break;
                case IconFA.WaveSquare: ret = 0xf83e; break;
                case IconFA.Waze: ret = 0xf83f; break;
                case IconFA.Weebly: ret = 0xf5cc; break;
                case IconFA.Weibo: ret = 0xf18a; break;
                case IconFA.Weight: ret = 0xf496; break;
                case IconFA.WeightHanging: ret = 0xf5cd; break;
                case IconFA.Weixin: ret = 0xf1d7; break;
                case IconFA.Whatsapp: ret = 0xf232; break;
                case IconFA.WhatsappSquare: ret = 0xf40c; break;
                case IconFA.Wheelchair: ret = 0xf193; break;
                case IconFA.Whmcs: ret = 0xf40d; break;
                case IconFA.Wifi: ret = 0xf1eb; break;
                case IconFA.WikipediaW: ret = 0xf266; break;
                case IconFA.Wind: ret = 0xf72e; break;
                case IconFA.WindowClose: ret = 0xf410; break;
                case IconFA.WindowMaximize: ret = 0xf2d0; break;
                case IconFA.WindowMinimize: ret = 0xf2d1; break;
                case IconFA.WindowRestore: ret = 0xf2d2; break;
                case IconFA.Windows: ret = 0xf17a; break;
                case IconFA.WineBottle: ret = 0xf72f; break;
                case IconFA.WineGlass: ret = 0xf4e3; break;
                case IconFA.WineGlassAlt: ret = 0xf5ce; break;
                case IconFA.Wix: ret = 0xf5cf; break;
                case IconFA.WizardsOfTheCoast: ret = 0xf730; break;
                case IconFA.Wodu: ret = 0xe088; break;
                case IconFA.WolfPackBattalion: ret = 0xf514; break;
                case IconFA.WonSign: ret = 0xf159; break;
                case IconFA.Wordpress: ret = 0xf19a; break;
                case IconFA.WordpressSimple: ret = 0xf411; break;
                case IconFA.Wpbeginner: ret = 0xf297; break;
                case IconFA.Wpexplorer: ret = 0xf2de; break;
                case IconFA.Wpforms: ret = 0xf298; break;
                case IconFA.Wpressr: ret = 0xf3e4; break;
                case IconFA.Wrench: ret = 0xf0ad; break;
                case IconFA.Xbox: ret = 0xf412; break;
                case IconFA.Xing: ret = 0xf168; break;
                case IconFA.XingSquare: ret = 0xf169; break;
                case IconFA.XRay: ret = 0xf497; break;
                case IconFA.Yahoo: ret = 0xf19e; break;
                case IconFA.Yammer: ret = 0xf840; break;
                case IconFA.Yandex: ret = 0xf413; break;
                case IconFA.YandexInternational: ret = 0xf414; break;
                case IconFA.Yarn: ret = 0xf7e3; break;
                case IconFA.YCombinator: ret = 0xf23b; break;
                case IconFA.Yelp: ret = 0xf1e9; break;
                case IconFA.YenSign: ret = 0xf157; break;
                case IconFA.YinYang: ret = 0xf6ad; break;
                case IconFA.Yoast: ret = 0xf2b1; break;
                case IconFA.Youtube: ret = 0xf167; break;
                case IconFA.YoutubeSquare: ret = 0xf431; break;
                case IconFA.Zhihu: ret = 0xf63f; break;
            }
            return ret;
        }
    }
}
