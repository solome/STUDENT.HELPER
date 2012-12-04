using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Specialized;

using Windows.Storage;

using System.Collections;
using System.IO;
using System.Xml;


namespace 学生助手.STUDENT.HELPER.Data
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class SampleDataCommon : 学生助手.STUDENT.HELPER.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public SampleDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description,
            String teacherName, String textBook, DateTime start = new DateTime(), DateTime end = new DateTime())
        {
            this._uniqueId = uniqueId;
            this._title = title;//课程名
            this._subtitle = subtitle;//开课地点
            this._description = description;//课程描述
            this._imagePath = imagePath;//课程图片路径
            this._teacherName = teacherName;//授课教师名
            this._textBook = textBook;//授课选用教材
            this._start = start; //开始时间
            this._end = end;    //结束时间
        }
        //ID
        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        //课程名
        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        //开课地点
        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        //课程描述
        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        //课程显示图片
        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(SampleDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        //教师姓名
        private string _teacherName = string.Empty;
        public string TeacherName
        {
            get { return this._teacherName; }
            set { this.SetProperty(ref this._teacherName, value); }
        }
        //选用教材
        private string _textBook = string.Empty;
        public string TextBook
        {
            get { return String.Format("《{0}》", this._textBook); }
            set { this.SetProperty(ref this._textBook, value); }
        }

        public DateTime _start;
        public DateTime _end;

        public string Time
        {
            get
            {
                return String.Format("|{0}:{1}-{2}:{3}",
                    _start.Hour, _start.Second, _end.Hour, _end.Second);
            }
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class SampleDataItem : SampleDataCommon
    {
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, SampleDataGroup group,
            String teacherName, String textBook, DateTime start = new DateTime(), DateTime end = new DateTime())
            : base(uniqueId, title, subtitle, imagePath, description, teacherName, textBook, start, end)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private SampleDataGroup _group;
        public SampleDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    public class SampleDataGroup : SampleDataCommon
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description,
            String teacherName, String textBook, DateTime start = new DateTime(), DateTime end = new DateTime())
            : base(uniqueId, title, subtitle, imagePath, description, teacherName, textBook, start, end)
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<SampleDataItem> _items = new ObservableCollection<SampleDataItem>();
        /// <summary>
        /// Items
        /// </summary>
        public ObservableCollection<SampleDataItem> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<SampleDataItem> _topItem = new ObservableCollection<SampleDataItem>();
        /// <summary>
        /// TopItems
        /// </summary>
        public ObservableCollection<SampleDataItem> TopItems
        {
            get { return this._topItem; }
        }
    }

    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _allGroups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<SampleDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _sampleDataSource.AllGroups;
        }

        public static SampleDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            var matches = _sampleDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public SampleDataSource()
        {
            String ITEM_CONTENT = String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}",
                        "行动是成功的阶梯，行动越多，登得越高。",
                        "没有口水与汗水，就没有成功的泪水。",
                        "任何的限制，都是从自己的内心开始的。",
                        "成功的法则极为简单，但简单并不代表容易。",
                        "做对的事情比把事情做对重要",
                        "苦想没盼头，苦干有奔头。",
                        "自己打败自己的远远多于比别人打败的。",
                        "忍耐力较诸脑力，尤胜一筹。", "环境不会改变，解决之道在于改变自己。");
            Random random = new Random();

            var group1 = new SampleDataGroup("1",
                    "星期日",//星期
                    "努力多一点，明天好一点",//励志名言
                    "Assets/" + random.Next(0, 30) + ".png",//背景图片
                    "当一个小小的心念变成成为行为时，便能成了习惯；从而形成性格，而性格就决定你一生的成败。", "失的猛", "ASP.NET网页设计");
            group1.Items.Add(new SampleDataItem("Group-1-Item-1",
                    "深入浅出WPF",
                    "北校区9#424",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "好好研究WPF技术，打好WPF技术会对学习Windows8和Windows phone开发提供方便。",
                    ITEM_CONTENT,
                    group1,
                    "失的猛", "ASP.NET网页设计"));
            group1.Items.Add(new SampleDataItem("Group-1-Item-2",
                   "深入浅出WPF",
                   "北校区9#424",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "好好研究WPF技术，打好WPF技术会对学习Windows8和Windows phone开发提供方便。",
                   ITEM_CONTENT,
                   group1,
                   "失的猛", "ASP.NET网页设计"));
            group1.Items.Add(new SampleDataItem("Group-1-Item-2",
                   "深入浅出WPF",
                   "北校区9#424",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "好好研究WPF技术，打好WPF技术会对学习Windows8和Windows phone开发提供方便。",
                   ITEM_CONTENT,
                   group1,
                   "失的猛", "ASP.NET网页设计"));
            group1.Items.Add(new SampleDataItem("Group-1-Item-2",
                   "深入浅出WPF",
                   "北校区9#424",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "好好研究WPF技术，打好WPF技术会对学习Windows8和Windows phone开发提供方便。",
                   ITEM_CONTENT,
                   group1,
                   "失的猛", "ASP.NET网页设计"));
            this.AllGroups.Add(group1);

            var group2 = new SampleDataGroup("2",
                    "星期一",
                    "执着是唯一的解药",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "这个世界并不是掌握在那些嘲笑者的手中，而恰恰掌握在能够经受得住嘲笑与批评忍不断往前走的人手中。",
                    "失的猛", "ASP.NET网页设计");
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                    "Java语言程序设计",
                    "北校区9#424",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "投资知识是明智的，投资网络中的知识就更加明智。没有天生的信心，只有不断培养的信心。",
                    ITEM_CONTENT,
                    group2, "失的猛", "ASP.NET网页设计"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                   "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                   "只有一条路不能选择——那就是放弃的路；只有一条路不能拒绝——那就是成长的路。",
                   ITEM_CONTENT,
                   group2, "失的猛", "ASP.NET网页设计"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                   "数据挖掘",
                   "南区10#712",
                  "Assets/" + random.Next(0, 30) + ".png",
                   "人的才华就如海绵的水，没有外力的挤压，它是绝对流不出来的。流出来后，海绵才能吸收新的源泉。",
                   ITEM_CONTENT,
                   group2, "失的猛", "ASP.NET网页设计"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                  "数据挖掘",
                   "南区10#712",
                  "Assets/" + random.Next(0, 30) + ".png",
                   "伟大的事业不是靠力气速度和身体的敏捷完成的，而是靠性格意志和知识的力量完成的。",
                   ITEM_CONTENT,
                   group2, "失的猛", "ASP.NET网页设计"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                 "数据挖掘",
                   "南区10#712",
                  "Assets/" + random.Next(0, 30) + ".png",
                   "肉体是精神居住的花园，意志则是这个花园的园丁。意志既能使肉体“贫瘠”下去，又能用勤劳使它“肥沃”起来。",
                   ITEM_CONTENT,
                   group2, "失的猛", "ASP.NET网页设计"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                  "数据挖掘",
                   "南区10#712",
                  "Assets/" + random.Next(0, 30) + ".png",
                   "为明天做准备的最好方法就是集中你所有智慧，所有的热忱，把今天的工作做得尽善尽美，这就是你能应付未来的唯一方法。",
                   ITEM_CONTENT,
                   group2, "失的猛", "ASP.NET网页设计"));
            this.AllGroups.Add(group2);

            var group3 = new SampleDataGroup("3",
                    "星期二",
                    "执着不惜",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "人性最可怜的就是：我们总是梦想着天边的一座奇妙的玫瑰园，而不去欣赏今天就开在我们窗口的玫瑰。",
                    "失的猛", "ASP.NET网页设计");
            group3.Items.Add(new SampleDataItem("Group-3-Item-1",
                  "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "这世上的一切都借希望而完成，农夫不会剥下一粒玉米，如果他不曾希望它长成种粒；单身汉不会娶妻，如果他不曾希望有孩子；商人也不会去工作，如果他不曾希望因此而有收益。",
                    ITEM_CONTENT,
                    group3,
                    "失的猛", "ASP.NET网页设计"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-1",
                    "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "障碍与失败，是通往成功最稳靠的踏脚石，肯研究利用它们，便能从失败中培养出成功。",
                    ITEM_CONTENT,
                    group3,
                    "失的猛", "ASP.NET网页设计"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-1",
                   "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "人生舞台的大幕随时都可能拉开，关键是你愿意表演，还是选择躲避。能把在面前行走的机会抓住的人，十有八九都会成功。",
                    ITEM_CONTENT,
                    group3,
                    "失的猛", "ASP.NET网页设计"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-1",
                  "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "相信就是强大，怀疑只会抑制能力，而信仰就是力量。那些尝试去做某事却失败的人，比那些什么也不尝试做却成功的人不知要好上多少。",
                    ITEM_CONTENT,
                    group3,
                    "失的猛", "ASP.NET网页设计"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-1",
                    "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "目标的坚定是性格中最必要的力量源泉之一，也是成功的利器之一。没有它，天才也会在矛盾无定的迷径中徒劳无功。",
                    ITEM_CONTENT,
                    group3,
                    "失的猛", "ASP.NET网页设计"));

            this.AllGroups.Add(group3);

            var group4 = new SampleDataGroup("4",
                    "星期三",
                    "智者一切求自己",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "没有什么事情有象热忱这般具有传染性，它能感动顽石，它是真诚的精髓。一个人几乎可以在任何他怀有无限热忱的事情上成功。",
                    "失的猛", "ASP.NET网页设计");

            group4.Items.Add(new SampleDataItem("Group-4-Item-6",
                  "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "挫折时，要像大树一样，被砍了，还能再长；也要像杂草一样，虽让人践踏，但还能勇敢地活下去。",
                    ITEM_CONTENT,
                    group4,
                    "失的猛", "ASP.NET网页设计"));
            group4.Items.Add(new SampleDataItem("Group-4-Item-6",
                  "数据挖掘",
                   "南区10#712",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "沿着阻力最小(也就最简单)的方向努力，就像从高处往山下滚雪球一样，看起来每滚一圈雪球都没有什么变化，但却会越滚越快，越滚越大，膨胀速度惊人。",
                    ITEM_CONTENT,
                    group4,
                    "失的猛", "ASP.NET网页设计"));
            group4.Items.Add(new SampleDataItem("Group-4-Item-6",
                    "网页设计",
                    "北区 12#123",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "告诉老师，你自己的水平如何，哪些题目可以不做。老师会理解的。但是不能完全丢开老师，去搞自己的一套。应该以老师的课堂教学和布置的作业为基础，结合自己的实际水平做一些微调。",
                    ITEM_CONTENT,
                    group4,
                    "失的猛", "ASP.NET网页设计"));
            group4.Items.Add(new SampleDataItem("Group-4-Item-6",
                       "网页设计",
                    "北区 12#123",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "讲究条理。将重要的学习用品和资料用书立或指向装好，分类存放，避免用时东翻西找。每天有天计划，每周有周计划，按计划有条不紊地做事，不一暴十寒。",
                    ITEM_CONTENT,
                    group4,
                    "失的猛", "ASP.NET网页设计"));
            group4.Items.Add(new SampleDataItem("Group-4-Item-6",
                      "网页设计",
                    "北区 12#123",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "以学为先。在他们心目中，学习是正事，理应先于娱乐，一心向学，气定神闲，心无旁骛，全力以赴，忘我备战。",
                    ITEM_CONTENT,
                    group4,
                    "失的猛", "ASP.NET网页设计"));
            this.AllGroups.Add(group4);

            var group5 = new SampleDataGroup("5",
                    "星期四",
                    "愚者一切求他人",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "成功呈概率分布，关键是你能不能坚持到成功开始呈现的那一刻。",
                    "失的猛", "ASP.NET网页设计");
            group5.Items.Add(new SampleDataItem("Group-5-Item-1",
                    "网页设计",
                    "北区 12#123",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "你可以这样理解impossible（不可能）——I'mpossible（我是可能的）。　",
                    ITEM_CONTENT,
                    group5, "失的猛", "ASP.NET网页设计"));
            group5.Items.Add(new SampleDataItem("Group-5-Item-1",
                  "网页设计",
                    "北区 12#123",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "如果你希望成功，以恒心为良友，以经验为参谋，以小心为兄弟，以希望为哨兵。",
                   ITEM_CONTENT,
                   group5, "失的猛", "ASP.NET网页设计"));
            group5.Items.Add(new SampleDataItem("Group-5-Item-1",
                    "网页设计",
                    "北区 12#123",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "任何的限制，都是从自己的内心开始的。忘掉失败，不过要牢记失败中的教训。",
                   ITEM_CONTENT,
                   group5, "失的猛", "ASP.NET网页设计"));
            group5.Items.Add(new SampleDataItem("Group-5-Item-1",
                    "网页设计",
                    "北区 12#123",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "每一日你所付出的代价都比前一日高，因为你的生命又消短了一天，所以每一日你都要更积极。今天太宝贵，不应该为酸苦的忧虑和辛涩的悔恨所销蚀，抬起下巴，抓住今天，它不再回来。",
                   ITEM_CONTENT,
                   group5, "失的猛", "ASP.NET网页设计"));
            group5.Items.Add(new SampleDataItem("Group-5-Item-1",
                   "网页设计",
                    "北区 12#123",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "环境永远不会十全十美，消极的人受环境控制，积极的人却控制环境。",
                   ITEM_CONTENT,
                   group5, "失的猛", "ASP.NET网页设计"));

            this.AllGroups.Add(group5);

            var group6 = new SampleDataGroup("6",
                    "星期五",
                    "放弃是魔鬼的举动",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "环境永远不会十全十美，消极的人受环境控制，积极的人却控制环境。",
                    "失的猛", "ASP.NET网页设计");
            group6.Items.Add(new SampleDataItem("Group-6-Item-1",
                    "Java程序设计语言",
                    "南区|12#324",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "拿望远镜看别人，拿放大镜看自己。",
                    ITEM_CONTENT,
                    group6, "失的猛", "ASP.NET网页设计"));
            group6.Items.Add(new SampleDataItem("Group-6-Item-1",
                   "Java程序设计语言",
                    "南区|12#324",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "竞争颇似打网球，与球艺胜过你的对手比赛，可以提高你的水平。（戏从对手来。）只有不断找寻机会的人才会及时把握机会。",
                    ITEM_CONTENT,
                    group6, "失的猛", "ASP.NET网页设计"));
            group6.Items.Add(new SampleDataItem("Group-6-Item-1",
                    "Java程序设计语言",
                    "南区|12#324",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "无论才能知识多么卓著，如果缺乏热情，则无异纸上画饼充饥，无补于事。",
                    ITEM_CONTENT,
                    group6, "失的猛", "ASP.NET网页设计"));
            group6.Items.Add(new SampleDataItem("Group-6-Item-1",
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "当一个小小的心念变成成为行为时，便能成了习惯；从而形成性格，而性格就决定你一生的成败。",
                    ITEM_CONTENT,
                    group6, "失的猛", "ASP.NET网页设计"));
            group6.Items.Add(new SampleDataItem("Group-6-Item-1",
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "这个世界并不是掌握在那些嘲笑者的手中，而恰恰掌握在能够经受得住嘲笑与批评忍不断往前走的人手中。",
                    ITEM_CONTENT,
                    group6, "失的猛", "ASP.NET网页设计"));

            this.AllGroups.Add(group6);
            var group7 = new SampleDataGroup("7",
                    "星期六",
                    "永不言弃",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "投资知识是明智的，投资网络中的知识就更加明智。没有天生的信心，只有不断培养的信心。",
                    "失的猛", "ASP.NET网页设计");
            group7.Items.Add(new SampleDataItem("Group-7-Item-1",
                    "数据结构与算法",
                    "南校区|10#467",
                    "Assets/" + random.Next(0, 30) + ".png",
                    "忍别人所不能忍的痛，吃别人所别人所不能吃的苦，是为了收获得不到的收获。",
                    ITEM_CONTENT,
                    group7,
                    "失的猛", "ASP.NET网页设计"));
            group7.Items.Add(new SampleDataItem("Group-7-Item-1",
                   "数据结构与算法",
                   "南校区|10#467",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "只有一条路不能选择——那就是放弃的路；只有一条路不能拒绝——那就是成长的路。",
                   ITEM_CONTENT,
                   group7,
                   "失的猛", "ASP.NET网页设计"));
            group7.Items.Add(new SampleDataItem("Group-7-Item-1",
                   "数据结构与算法",
                   "南校区|10#467",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "人的才华就如海绵的水，没有外力的挤压，它是绝对流不出来的。流出来后，海绵才能吸收新的源泉。",
                   ITEM_CONTENT,
                   group7,
                   "失的猛", "ASP.NET网页设计"));
            group7.Items.Add(new SampleDataItem("Group-7-Item-1",
                   "数据结构与算法",
                   "南校区|10#467",
                   "Assets/" + random.Next(0, 30) + ".png",
                   "伟大的事业不是靠力气速度和身体的敏捷完成的，而是靠性格意志和知识的力量完成的。",
                   ITEM_CONTENT,
                   group7,
                   "失的猛", "ASP.NET网页设计"));
            this.AllGroups.Add(group7);
   
            var group_for_a_test = new SampleDataGroup("10",
                 "测试项",//星期
                    "努力多一点，明天好一点",//励志名言
                    "Assets/" + random.Next(0, 30) + ".png",//背景图片
                    "当一个小小的心念变成成为行为时，便能成了习惯；从而形成性格，而性格就决定你一生的成败。", "失的猛", "ASP.NET网页设计");
            group_for_a_test.Items.Add(new SampleDataItem("Group-8-Item-1", "C#入门经典", "10&560",
                "Assets/" + random.Next(0, 30) + ".png",
                "Just for a test",
                "测试效果：此部分为课程的简单描述",
                group_for_a_test,
                "小白菜",
                "C#入门经典 2012年版"));

            this.AllGroups.Add(group_for_a_test);

        }
    }
}
