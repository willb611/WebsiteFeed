var FeedList = React.createClass({
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
                <Feed title={feed.Title} link={feed.Link} summary={feed.Summary} category={feed.Category} datetime={feed.DateTime}/>
            );
        });
        return (
          <div className="feedList">
              {feedItems}
          </div>
      );
    }
});

ReactDOM.render(
    <FeedList url="/feeds/runescape" pollInterval="10000"/>,
  document.getElementById('content')
);
