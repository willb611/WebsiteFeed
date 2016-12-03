import React from 'react';
import renderer from 'react-test-renderer';
import PostList from '../Scripts/PostList.jsx';

describe('Welcome (Snapshot)', () => {
    it('Welcome renders PostList', () => {
        const someData = [];
        const component = renderer.create(<PostList data={someData}/>);
        const json = component.toJSON();
        expect(json).toMatchSnapshot();
    });
});
