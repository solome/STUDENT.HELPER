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
using SQLite;

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
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
            this._teacherName = teacherName;
            this._textBook = textBook;
            this._start = start; 
            this._end = end;   
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
            get { return String.Format("{0}", this._textBook); }
            set { this.SetProperty(ref this._textBook, value); }
        }

        public DateTime _start;
        public DateTime _end;

        public string Time
        {
            get
            {
                return String.Format("|{0}-{1}",
                    _start.ToString("HH:mm"), _end.ToString("HH:mm"));
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
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description = "",
            String teacherName = "", String textBook = "", DateTime start = new DateTime(), DateTime end = new DateTime())
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

            string[] INIT_QUEO = { 
                "喜欢读书，就等于把生活中寂寞的辰光换成巨大享受的时刻。 —— 孟德斯鸠", 
                "勤奋可以弥补聪明的不足，但聪明无法弥补懒惰的缺陷。", 
                "我学习了一生，现在我还在学习，而将来，只要我还有精力，我还要学习下去。 -----别林斯基", 
                "努力向上的开拓，才使弯曲的竹鞭化作了笔直的毛竹。",  
                "古之立大事者，不惟有超世之才，亦必有坚忍不拔之志。", 
                "天将降大任于是人也，必先苦其心志，劳其筋骨，饿其体肤，空乏其身，行拂乱其所为。", 
                "在劳力上劳心，是一切发明之母。事事在劳力上劳心，变可得事物之真理。", 
                "茂盛的禾苗需要水分;成长的少年需要学习。", 
                "勤奋的含义是今天的热血，而不是明天的决心，后天的保证。", 
                "有事者，事竟成；破釜沉舟，百二秦关终归楚；苦心人，天不负；卧薪尝胆，三千越甲可吞吴。", 
                "积极的人在每一次忧患中都看到一个机会，而消极的人则在每个机会都看到某种忧患。", 
                "伟人之所以伟大，是因为他与别人共处逆境时，别人失去了信心，他却下决心实现自己的目标。", 
                "当你感到悲哀痛苦时，最好是去学些什么东西。学习会使你永远立于不败之地。", 
                "如果你希望成功，以恒心为良友，以经验为参谋，以小心为兄弟，以希望为哨兵。 ", 
                "一个能从别人的观念来看事情，能了解别人心灵活动的人，永远不必为自己的前途担心。", 
                "环境永远不会十全十美，消极的人受环境控制，积极的人却控制环境。 ", 
                "事实上，成功仅代表了你工作的1%，成功是99%失败的结果。 ", 
                "竞争颇似打网球，与球艺胜过你的对手比赛，可以提高你的水平。", 
                "你可以选择这样的“三心二意”：信心、恒心、决心；创意、乐意。", 
                "当你还不能对自己说今天学到了什么东西时，你就不要去睡觉。", 
                "学然后知不足，教然后知困。知不足，然后能自反也；知困，然后能自强也。", 
                "做学问的功夫，是细嚼慢咽的功夫。好比吃饭一样，要嚼得烂，方好消化，才会对人体有益。", 
                "须交有道之人,莫结无义之友。饮清静之茶，莫贪花色之酒。开方便之门，闲是非之口。", 
                "罗马人凯撒大帝，威震欧亚非三大陆，临终告诉侍者说：“请把我的双手放在棺材外面，让世人看看，伟大如我凯撒者，死后也是两手空空。", 
                "生气，就是拿别人的过错来惩罚自己。原谅别人，就是善待自己。", 
                "我不去想是否能够成功，既然选择了远方，便只顾风雨兼程；我不去想身后会不会袭来寒风冷雨，既然目标是地平线，留给世界的只能是背影。", 
                "现在睡觉的话会做梦 而现在学习的话会让梦实现。", 
                "摒弃侥幸之念，必取百炼成钢；厚积分秒之功，始得一鸣惊人。", 
                "惜光阴百日犹短，看众志成城拼搏第一；细安排一刻也长，比龙争虎斗谁为争锋？！",
                "没有一颗心，会因为努力生活而受伤。当你真心感恩生活努力想过好每一天的时候，全宇宙都会来帮忙。"};

            Random random = new Random();

            int GroupIndex = random.Next(0, 30);
            int GroupItemIndex = random.Next(0, 30);
            int INIT_QUEOIndex = random.Next(0, 30);
            int le = INIT_QUEO.Length;
            Messages temp;
            List<object> list = App.db.Query(new TableMapping(typeof(Messages)), "select * From messages where week = '0' ");
            var groupSunday = new SampleDataGroup("Sunday","星期日","努力多一点，明天好一点","Assets/" + (GroupIndex++) % 31 + ".png");
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    temp = (Messages)list[i];
                    groupSunday.Items.Add(new SampleDataItem("Group-Sunday" + i, temp.Title,
                           temp.Subtitle, "Assets/" + (GroupItemIndex++) % 31 + ".png",
                           INIT_QUEO[((INIT_QUEOIndex++) % 30)], temp.Description,
                           groupSunday,
                           temp.Teacher, temp.TextBook, DateTime.Parse(temp.Start), DateTime.Parse(temp.End)));
                }
            }
            this.AllGroups.Add(groupSunday);

            list = App.db.Query(new TableMapping(typeof(Messages)), "select * From messages where week = '4' ");
            var groupThursday = new SampleDataGroup("Thursday", "星期四", "青春无悔，拼搏最美", "Assets/" + (GroupIndex++) % 31 + ".png");
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    temp = (Messages)list[i];
                    groupThursday.Items.Add(new SampleDataItem("Group-Thursday" + i, temp.Title,
                           temp.Subtitle, "Assets/" + (GroupItemIndex++) % 31 + ".png",
                           INIT_QUEO[((INIT_QUEOIndex++) % 30)], temp.Description,
                           groupThursday,
                           temp.Teacher, temp.TextBook, DateTime.Parse(temp.Start), DateTime.Parse(temp.End)));
                }
            }
            this.AllGroups.Add(groupThursday);

            list = App.db.Query(new TableMapping(typeof(Messages)), "select * From messages where week = '1' ");
            var groupMonday = new SampleDataGroup("Monday", "星期一", "天行健，君子以自强不息", "Assets/" + (GroupIndex++) % 31 + ".png");
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    temp = (Messages)list[i];
                    groupMonday.Items.Add(new SampleDataItem("Group-Monday" + i, temp.Title,
                           temp.Subtitle, "Assets/" + (GroupItemIndex++) % 31 + ".png",
                           INIT_QUEO[((INIT_QUEOIndex++) % 30)], temp.Description,
                           groupMonday,
                           temp.Teacher, temp.TextBook,DateTime.Parse(temp.Start),DateTime.Parse(temp.End)));
                }
            }
            this.AllGroups.Add(groupMonday);

            list = App.db.Query(new TableMapping(typeof(Messages)), "select * From messages where week = '5' ");
            var groupFriday = new SampleDataGroup("Friday", "星期五", "十年树木,百年树人", "Assets/" + (GroupIndex++) % 31 + ".png");
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    temp = (Messages)list[i];
                    groupFriday.Items.Add(new SampleDataItem("Group-Friday" + i, temp.Title,
                           temp.Subtitle, "Assets/" + (GroupItemIndex++) % 31 + ".png",
                           INIT_QUEO[((INIT_QUEOIndex++) % 30)], temp.Description,
                           groupMonday,
                           temp.Teacher, temp.TextBook, DateTime.Parse(temp.Start), DateTime.Parse(temp.End)));
                }
            }
            this.AllGroups.Add(groupFriday);

            list = App.db.Query(new TableMapping(typeof(Messages)), "select * From messages where week = '2' ");
            var groupTuesday = new SampleDataGroup("Tuesday", "星期二", "绳锯木断，水滴石穿", "Assets/" + (GroupIndex++) % 31 + ".png");
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    temp = (Messages)list[i];
                    groupTuesday.Items.Add(new SampleDataItem("Group-Tuesday" + i, temp.Title,
                           temp.Subtitle, "Assets/" + (GroupItemIndex++) % 31 + ".png",
                           INIT_QUEO[((INIT_QUEOIndex++) % 30)], temp.Description,
                           groupTuesday,
                           temp.Teacher, temp.TextBook, DateTime.Parse(temp.Start), DateTime.Parse(temp.End)));
                }
            }
            this.AllGroups.Add(groupTuesday);           

            list = App.db.Query(new TableMapping(typeof(Messages)), "select * From messages where week = '6' ");
            var groupSaturday = new SampleDataGroup("Saturday", "星期六", "莫等闲,白了少年头,空悲切", "Assets/" + (GroupIndex++) % 31 + ".png");
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    temp = (Messages)list[i];
                    groupSaturday.Items.Add(new SampleDataItem("Group-Saturday" + i, temp.Title,
                           temp.Subtitle, "Assets/" + (GroupItemIndex++) % 31 + ".png",
                           INIT_QUEO[((INIT_QUEOIndex++) % 30)], temp.Description,
                           groupMonday,
                           temp.Teacher, temp.TextBook, DateTime.Parse(temp.Start), DateTime.Parse(temp.End)));
                }
            }
            this.AllGroups.Add(groupSaturday);

            list = App.db.Query(new TableMapping(typeof(Messages)), "select * From messages where week = '3' ");
            var groupWednesday = new SampleDataGroup("Wednesday", "星期三", "博瞧而约取，厚积而薄发", "Assets/" + (GroupIndex++) % 31 + ".png");
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    temp = (Messages)list[i];
                    groupWednesday.Items.Add(new SampleDataItem("Group-Wednesday" + i, temp.Title,
                           temp.Subtitle, "Assets/" + ((GroupItemIndex++) % 31) + ".png",
                           INIT_QUEO[((INIT_QUEOIndex++) % 30)], temp.Description,
                           groupWednesday,
                           temp.Teacher, temp.TextBook, DateTime.Parse(temp.Start), DateTime.Parse(temp.End)));
                }
            }
            this.AllGroups.Add(groupWednesday);
        }
    }
}
