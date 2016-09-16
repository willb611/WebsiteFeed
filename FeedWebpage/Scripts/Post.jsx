var React = require('react');

var Post = React.createClass({
    render: function () {
        var dateTimeString = new Date(parseInt(this.props.datetime.replace("/Date(", "").replace(")/", ""), 10)).toDateString();
        return (
          <div className="post">
              <h4 className="title"><a href={this.props.link}>{this.props.title}</a></h4>
              <h5 className="category">Category: {this.props.category}</h5>
              <p className="summary">{this.props.summary}</p>
              <p className="postDate">Posted at: <time className="timePosted">{dateTimeString}</time></p>
          </div>
      );
    }
});
