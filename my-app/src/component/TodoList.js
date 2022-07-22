import '../App.css';

import { Container, Row, Col } from 'react-grid-system';
const TodoList = (props) => {
    return (

        <div>
            <h3>Todo</h3>
            <table>
                <thead>
                    <Container>
                        <Row>
                            <Col xl={6}>Item Name </Col>
                            <Col xl={3}>Edit </Col>
                            <Col xl={3}>Delete </Col>

                        </Row>
                    </Container>
                </thead>
                <tbody>
                    {props.tableData.length > 0 ? (props.tableData.map((item) => {

                        return item.status === 'todo' && (
                            <Container>
                                <Row>
                                    <Col xl={6} xm={8} xs sm>
                                        <input type="checkbox" onChange={() => props.checkBoxClick(item.id)} />{item.name}
                                    </Col>
                                    <Col xl={3} xm={2} xs sm>
                                        <button className="edit-btn" type="button" onClick={() => { props.editRow(item) }}>Edit</button>
                                    </Col>
                                    <Col xl={3} xm={2} xs sm>
                                        <button className="edit-btn" type="button" onClick={() => props.deleteItem(item.id)}>Delete</button>
                                    </Col>

                                </Row>
                            </Container>

                        );
                    })
                    ) : (
                        <tr>
                              <td className='red' colSpan={3}>No Massage</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    )
}

export default TodoList