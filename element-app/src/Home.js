function Home() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Todo />}></Route>
        <Route path="/TodoList" element={<TodoList />}></Route>
        <Route path="/TodoTable" element={<TodoTable />}></Route>
        <Route path="/TodoItem" element={<TodoItem />}></Route>
        <Route path="/PracticeTodo" element={<PracticeTodo />}></Route>
        <Route path="/Grid" element={<Grid />}></Route>
        <Route path="/AgGrid" element={<AgGrid />}></Route>
        <Route path="/Table" element={<Table />}></Route>
        <Route path="/Table1" element={<Table1 />}></Route>
        <Route path="/EditBtn" element={<EditBtn />}></Route>
        <Route path="/TableChild" element={<TableChild />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default Home;
