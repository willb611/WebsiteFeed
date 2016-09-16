var React = require('react');
var PostList = require('./PostList');


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
