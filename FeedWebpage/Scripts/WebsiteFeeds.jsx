var WebsiteFeed = React.createClass({
    loadDataFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data.Posts });
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
        return (
            <div className="websiteFeed">
                <h2>News for {this.props.websiteName}</h2>
                <PostList data={this.state.data} />
            </div>
      );
    }
});

var Websites = React.createClass({
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
            <ul className="websites">{websiteFeeds}</ul>
        );
    }
});


ReactDOM.render(
    <Websites />,
    document.getElementById('content')
);
