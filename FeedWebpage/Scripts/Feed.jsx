
var Feed = React.createClass({
    loadDataFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentDidMount: function () {
        this.loadDataFromServer();
        window.setInterval(this.loadDataFromServer, this.props.pollInterval);
    },
    render: function () {
        return (
          <div className="feed">
              Example news feed!
          </div>
      );
}
});


ReactDOM.render(
    <Feed url="/feeditem" pollInterval="10000"/>,
  document.getElementById('content')
);
