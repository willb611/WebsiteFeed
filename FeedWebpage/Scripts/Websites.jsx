var React = require('react');
var WebsiteFeed = require('./WebsiteFeed');

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

module.exports = Websites;
