var React = require('react');
var Post = require('./Post');

var PostList = React.createClass({
    getDefaultProps: function () {
        return { data: [] };
    },
    render: function () {
        var feedItems = this.props.data.map(function (feed) {
            return (
                <li>
                    <Post title={feed.Title} link={feed.Link} summary={feed.Summary} category={feed.Category} datetime={feed.DateTime } />
                </li>
            );
        });
        return (
<ul className="postList">
    {feedItems}
</ul>
        );
    }
});

module.exports = PostList;
