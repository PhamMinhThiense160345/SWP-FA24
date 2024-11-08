using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Helpers;

public class CommentHelper : ICommentHelper
{
    public ResponseView CheckContent(string content)
    {
        if (ContainsRestrictedWord(content, ViolentWords))
            return new ResponseView(false, "Invalid content: contains violent language.");

        if (ContainsRestrictedWord(content, VulgarWords))
            return new ResponseView(false, "Invalid content: contains vulgar language.");

        if (ContainsRestrictedWord(content, AdultWords))
            return new ResponseView(false, "Invalid content: contains adult language.");

        if (ContainsRestrictedWord(content, AntiSocialWords))
            return new ResponseView(false, "Invalid content: contains anti-social language.");

        return new ResponseView(true, "Content is valid.");
    }

    #region private 
    private readonly List<string> ViolentWords = new List<string>
    {
        // Vietnamese
        "giết", "đánh", "đập", "thù", "trả thù", "tấn công", "hạ gục", "sát hại",
        "hành hung", "khủng bố", "giết người", "thanh trừng", "hãm hại", "hủy diệt",
        "đấm đá", "đâm chém", "đánh nhau", "tàn sát", "chém giết", "phá hoại",
        "khống chế", "chiến tranh", "thảm sát", "đàn áp", "hành quyết", "hủy hoại",
        "xử lý", "đe dọa", "xâm chiếm", "đánh chiếm", "khủng bố", "cướp bóc", "bạo lực",
        "phá hủy", "trả đũa", "thủ tiêu", "khủng bố hóa", "bắt cóc", "bạo loạn",
    
        // English
        "kill", "murder", "attack", "assault", "terrorist", "terrorism", "fight",
        "violence", "destroy", "revenge", "execute", "massacre", "bomb", "war",
        "slaughter", "shoot", "stab", "beat up", "hurt", "harm", "destruction",
        "threaten", "invade", "capture", "abduct", "lynch", "assassinate", "genocide",
        "vandalism", "loot", "pillage", "violence", "hostage", "uprising", "conflict",
        "bloodshed", "riot", "suppress", "inflict", "injure", "execute", "militia",
        "revolt", "rebellion", "bloodbath", "torment", "incite", "hostility",
        "brutality", "torture", "extortion", "arm", "force", "coerce", "oppression",
        "hatred", "rage", "havoc", "onslaught", "conquer", "occupy", "terrorize"
    };


    // Từ ngữ thô tục (Tiếng Việt và Tiếng Anh)
    private readonly List<string> VulgarWords = new List<string>
    {
        // Vietnamese
        "c*", "đ*", "thằng", "con", "mẹ mày", "khốn nạn", "mất dạy", "ngu dốt",
        "dốt", "đần", "bỉ ổi", "khốn", "chửi", "đụng chạm", "chó má", "láo toét",
        "ngu xuẩn", "ngu si", "dốt nát", "hãm", "đần độn", "óc chó", "não cá vàng",
        "súc sinh", "bẩn thỉu", "bố láo", "vô học", "dốt đặc", "thô tục", "đần thối",
        "ngu ngốc", "não phẳng", "mất nết", "hỗn láo", "vô liêm sỉ", "khốn kiếp",
        "láo lếu", "kẻ hèn", "mất gốc", "bẩn bựa", "đồ điên", "ngu người", "xỏ lá",
        "mất dạy", "hạ tiện", "đồ đê tiện", "trời đánh", "phản phúc", "mặt dày",
        "đồ lươn lẹo", "cặn bã", "ngu dốt", "tồi tệ",

        // English
        "damn", "hell", "bastard", "jerk", "idiot", "stupid", "dumb", "bitch",
        "shit", "asshole", "dick", "suck", "crap", "piss off", "freak", "moron",
        "retard", "scum", "pig", "dirty", "stinking", "filthy", "prick", "twat",
        "arsehole", "loser", "jerk-off", "trash", "scumbag", "slag", "slut",
        "degenerate", "fool", "nitwit", "cretin", "blockhead", "nincompoop",
        "jackass", "imbecile", "dirtbag", "low-life", "sleazebag", "pervert",
        "slime", "grub", "stupid-head", "clown", "insignificant", "dirt", "clod",
        "dog", "menace", "snob", "parasite", "incompetent", "useless", "pathetic",
        "numbskull", "thick-skull", "pea-brain", "foul", "ignorant", "shameless"
    };


    private readonly List<string> AdultWords = new List<string>
    {
        // Vietnamese
        "sex", "nhạy cảm", "đồ chơi người lớn", "xxx", "khiêu dâm", "dâm dục",
        "đồi trụy", "gái gọi", "mại dâm", "khẩu giao", "hoan lạc", "trần truồng",
        "làm tình", "quan hệ", "tình dục", "sờ mó", "hôn hít", "ngực", "mông",
        "khỏa thân", "phim cấp 3", "hở hang", "kích dục", "kích thích", "thủ dâm",
        "tự sướng", "bóc lột tình dục", "dâm đãng", "bạo dâm", "cuồng dâm",
        "kích thích tình dục", "dục vọng", "hưng phấn", "gợi cảm", "bê đê",
        "mông to", "cởi truồng", "cởi đồ", "bán dâm", "xem phim người lớn",
        "nhu cầu tình dục", "dâm ô", "gái mại dâm", "thác loạn", "loạn luân",

        // English
        "porn", "erotic", "adult toy", "xxx", "sexual", "nudity", "nude",
        "prostitute", "sex", "intimate", "breast", "naked", "hooker",
        "orgasm", "strip", "flirt", "kiss", "underwear", "expose",
        "fetish", "seductive", "lust", "masturbation", "voyeur", "sensual",
        "adult content", "racy", "dirty", "pleasure", "hentai", "taboo",
        "explicit", "seduction", "provocative", "temptation", "satisfaction",
        "arousal", "sultry", "hot", "naughty", "dominatrix", "stripper",
        "escort", "erotica", "body", "pole dance", "lingerie", "bikini",
        "foreplay", "kinky", "sensuality", "fantasy", "threesome",
        "sex toy", "intimacy", "kamasutra", "fetishism", "exhibitionist"
    };


    // Từ ngữ chống phá (Tiếng Việt và Tiếng Anh)
    private readonly List<string> AntiSocialWords = new List<string>
    {
        // Tiếng Việt
        "phản động", "bạo loạn", "lật đổ", "đảo chính", "kích động", "tẩy chay",
        "chống chính phủ", "tụ tập đông người", "hô hào", "phá hoại", "ly khai",
        "bất tuân", "nổi loạn", "kích động bạo lực", "bài xích", "công kích",
        "chính phủ bù nhìn", "chế độ độc tài", "nổi dậy",
        // English
        "revolt", "protest", "insurgent", "overthrow", "rebellion", "resist",
        "boycott", "coup", "government overthrow", "anarchy", "sedition",
        "riots", "rebel", "non-compliance", "anti-government", "attack",
        "disrupt", "radical", "subversion", "regime change"
    };

    private bool ContainsRestrictedWord(string content, List<string> restrictedWords)
    {
        foreach (var word in restrictedWords)
        {
            if (content.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}