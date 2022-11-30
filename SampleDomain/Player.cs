using Domain.Tests;

namespace Domain;

public class Player : IEntity
{
    private Player(int playerId, int influenceRankType, string creativityRank, int creativityRankType, int threatRank, int threatRankType, Guid id)
    {
        _playerId = playerId;
        _influenceRankType = influenceRankType;
        _creativityRank = creativityRank;
        _creativityRankType = creativityRankType;
        _threatRank = threatRank;
        _threatRankType = threatRankType;
        Id = id;
    }

    public static Player Create(int playerId, int influenceRankType, string creativityRank, int creativityRankType, int threatRank, int threatRankType, Guid id)
    {
        return new Player( playerId, influenceRankType, creativityRank, creativityRankType, threatRank, threatRankType, id);
    }

    public Guid Id { get; }
    private int? _chanceOfPlayingNextRound;
    public int? ChanceOfPlayingNextRound => _chanceOfPlayingNextRound;

    private int? _chanceOfPlayingThisRound;
    public int? ChanceOfPlayingTheRound => _chanceOfPlayingNextRound;

    private int _code;
    public int Code => _code;

    private int _costChangeEvent;
    public int CostChangeEvent => _costChangeEvent;

    private int _costChangeEventFall;
    public int CostChangeEventFall => _costChangeEventFall;

    private int _costChangeStart;
    public int CostChangeStart => _costChangeStart;

    private int _costChangeStartFall;
    public int CostChangeStartFall => _costChangeStartFall;

    private int _dreamTeamCount;
    public int DreamTeamCount => _dreamTeamCount;

    private int _elementType;
    public int ElementType => _elementType;

    private string _epNext;
    public string EpNext => _epNext;

    private string _epThis;
    public string EpThis => _epThis;

    private int _eventPoints;
    public int EventPoints => _eventPoints;

    private string _firstName;
    public string FirstName => _firstName;

    private string? _form;
    public string? Form => _form;

    private int _playerId;
    public int PlayerId => _playerId;

    private bool _inDreamTeam;
    public bool InDreamTeam => _inDreamTeam;

    private string _news;
    public string News => _news;

    private DateTime? _newsAdded;
    public DateTime? NewsAdded => _newsAdded;

    private int _nowCost;
    public int NowCost => _nowCost;

    private string _photo;
    public string Photo => _photo;

    private string _pointsPerGame;
    public string PointsPerGame => _pointsPerGame;

    private string _secondGame;
    public string SecondGame => _secondGame;

    private string _selectedByPercent;
    public string SelectedByPercent => _selectedByPercent;

    private bool _special;
    public bool Special => _special;

    private int? _squadNumber;
    public int? SquadNumber => _squadNumber;

    private string _status;
    public string Status => _status;

    private int _team;
    public int Team => _team;

    private int _teamCode;
    public int TeamCode => _teamCode;

    private int _totalPoints;
    public int TotalPoints => _totalPoints;

    private int _transfersIn;
    public int TransfersIn => _transfersIn;

    private int _transfersInEvent;
    public int TransfersInEvent => _transfersInEvent;

    private int _transfersOut;
    public int TransfersOut => _transfersIn;

    private int _transfersOutEvent;
    public int TransfersOutEvent => _transfersOutEvent;

    private string _valueForm;
    public string ValueForm => _valueForm;

    private string _valueSeason;
    public string ValueSeason => _valueSeason;

    private string _webName;
    public string WebName => _webName;

    private int _minutes;
    public int Minutes => _minutes;

    private int _goalsScored;
    public int GoalsScored => _goalsScored;

    private int _assists;
    public int Assists => _assists;

    private int _cleanSheets;
    public int CleanSheets => _cleanSheets;

    private int _goalsConnected;
    public int GoalsConnected => _goalsConnected;

    private int _ownGoals;
    public int OwnGoals => _ownGoals;

    private int _penaltiesSaved;
    public int PenlatiesSaved => _penaltiesSaved;

    private int _penaltiesMissed;
    public int PenaltiesMissed => _penaltiesMissed;

    private int _yellowCards;
    public int YellowCards => _yellowCards;

    private int _redCards;
    public int RedCards => _redCards;

    private int _saves;
    public int Saves => _saves;

    private int _bonus;
    public int Bonus => _bonus;

    private int _bps;
    public int Bps => _bps;

    private string _influence;
    public string Influence => _influence;

    private string _creativity;
    public string Creativity => _creativity;

    private string _threat;
    public string Threat => _threat;

    private string _ictIndex;
    public string IctIndex => _ictIndex;

    private int _influenceRank;
    public int InfluenceRank => _influenceRank;

    private int _influenceRankType;
    public int InfluenceRankType => _influenceRankType;

    private string _creativityRank;
    public string CreativityRank => _creativityRank;

    private int _creativityRankType;
    public int CreativityRankType => _creativityRankType;

    private int _threatRank;
    public int ThreatRankRank => _threatRank;

    private int _threatRankType;
    public int ThreatRankType => _threatRankType;

    private int _ictRank;
    public int IctRankRank => _ictRank;

    private int _ictRankType;
    public int IctRankType => _ictRankType;

    private int? _cornersAndIndirectFreeKicksOrder;
    public int? CornersAndIndirectFreeKicksOrder => _cornersAndIndirectFreeKicksOrder;

    private string _cornersAndIndirectFreeKicksText;
    public string CornersAndIndirectFreeKicksText => _cornersAndIndirectFreeKicksText;

    private int? _directFreeKicksOrder;
    public int? DirectFreeKicksOrder => _directFreeKicksOrder;

    private string _directFreeKicksText;
    public string DirectFreeKicksText => _directFreeKicksText;

    private int? _penaltiesOrder;
    public int? PenaltiesOrder => _penaltiesOrder;

    private string _penaltiesText;
    public string PenaltiesText => _penaltiesText;

    
    
}