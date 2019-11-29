import Axios from 'axios'

const Api = Axios.create({ baseURL : 'https://jsonplaceholder.typicode.com'});

export default Api;

