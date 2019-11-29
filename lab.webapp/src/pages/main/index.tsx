import React, { Component } from 'react'
import Api from '../../services/api'

export default class Main extends Component {

    state = {
        posts: [
            {
                id: '',
                title: '',
                body: ''
            }
        ]
    };

    componentDidMount() {
        this.loadData();
    }

    loadData = async () => {
        const response = await Api.get('/Posts');
        this.setState({ posts: response.data });
    }

    render() {
        const { posts } = this.state;

        return (
            <div className="post-list">
                {posts.map(post => (
                    <article key={post.id}>
                        <p>{post.title}</p>
                        <p>{post.body}</p>
                    </article>
                ))}
            </div>
        )
    }
}