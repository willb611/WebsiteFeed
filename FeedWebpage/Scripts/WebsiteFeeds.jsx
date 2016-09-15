var WebsiteFeed = React.createClass({
    loadDataFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            console.log(data.Feeds);
            this.setState({ data: data.Feeds });
        }.bind(this);
        xhr.send();
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentDidMount: function () {
        setTimeout(this.loadDataFromServer(), 0);
        window.setInterval(this.loadDataFromServer, this.props.pollInterval);
    },
    render: function () {
        var feedItems = this.state.data.map(function (feed) {
            console.log("[FeedList] Making feed from: " + feed);
            return (
                <li>
                    <Post title={feed.Title} link={feed.Link} summary={feed.Summary} category={feed.Category} datetime={feed.DateTime } />
                </li>
            );
        });
        return (
            <div className="feedListContainer">
                <h2>News for {this.props.websiteName}</h2>
                <ul className="feedList">
                    {feedItems}
                </ul>
            </div>
      );
    }
});

var WebsiteFeedList = React.createClass({
    getInitialState: function () {
        return {
            websites: [
                { url: '/feeds/runescape', pollInterval: '10000', websiteName: 'Runescape' },
                { url: '/feeds/techcrunch', pollInterval: '10000', websiteName: 'Techcrunch' }
            ]
        };
    },
    render: function () {
        var websiteFeeds = this.state.websites.map(function (feed) {
            return (
                <li>
                    <WebsiteFeed url={feed.url} pollInterval={feed.pollInterval} websiteName={feed.websiteName} />
                </li>
            );
        });
        return (
            <li className="websiteList">{websiteFeeds}</li>
        );
    }
});


ReactDOM.render(
    <WebsiteFeedList />,
    document.getElementById('content')
);
