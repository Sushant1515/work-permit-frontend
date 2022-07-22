import React from "react";
import {
  Grid,
  GridColumn as Column,
  GridToolbar,
} from "@progress/kendo-react-grid";
import { MyCommandCell } from "./myCommandCell";
import { DropDownList } from "@progress/kendo-react-dropdowns";
import { insertItem, getItems, updateItem, deleteItem } from "./services";
import { filterBy } from "@progress/kendo-data-query";

import {
  IntlProvider,
  load,
  LocalizationProvider,
  loadMessages,
  IntlService,
} from "@progress/kendo-react-intl";
import { sampleProducts } from "./sample-products";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

const editField = "inEdit";
const intialstate = {
  logic: "and",
  filters: [],
};

const New_workPermit = () => {
  const locales = [
    {
      language: "en-US",
      locale: "en",
    },
    {
      language: "es-ES",
      locale: "es",
    },
  ];
  const { t, i18n } = useTranslation();

  const [data, setData] = React.useState([]);
  const navigate = useNavigate();
  const [filter, setFilter] = React.useState(intialstate);
  const [currentLocale, setCurrentLocale] = React.useState(locales[0]);
  React.useEffect(() => {
    let newItems = getItems();
    setData(newItems);
  }, []); // modify the data in the store, db etc

  const remove = (dataItem) => {
    const newData = data.filter((item) => item.ProductID != dataItem.ProductID);
    setData(newData);
  };

  const add = (dataItem) => {
    dataItem.inEdit = true;
    const newData = insertItem(dataItem);
    setData(newData);
  };

  const update = (dataItem) => {
    dataItem.inEdit = false;
    const newData = updateItem(dataItem);
    setData(newData);
  }; // Local state operations

  const discard = () => {
    const newData = [...data];
    newData.splice(0, 1);
    setData(newData);
  };

  const handalGotoPage = () => {
    navigate("/CreateNewWorkpermit");
  };
  const cancel = (dataItem) => {
    const originalItem = getItems().find(
      (p) => p.ProductID === dataItem.ProductID
    );
    const newData = data.map((item) =>
      item.ProductID === originalItem.ProductID ? originalItem : item
    );
    setData(newData);
  };

  const enterEdit = (dataItem) => {
    setData(
      data.map((item) =>
        item.ProductID === dataItem.ProductID ? { ...item, inEdit: true } : item
      )
    );
  };

  const itemChange = (event) => {
    const newData = data.map((item) =>
      item.ProductID === event.dataItem.ProductID
        ? { ...item, [event.field || ""]: event.value }
        : item
    );
    setData(newData);
  };

  const addNew = () => {
    const newDataItem = {
      inEdit: true,
      Discontinued: false,
    };
    setData([newDataItem, ...data]);
  };

  const handleChange = (e) => {
    i18n.changeLanguage(e.target.value.locale);
  };

  const CommandCell = (props) => (
    <MyCommandCell
      {...props}
      edit={enterEdit}
      remove={remove}
      add={add}
      discard={discard}
      update={update}
      cancel={cancel}
      editField={editField}
    />
  );
  return (
    <Grid
      onItemChange={itemChange}
      editField={editField}
      filterable={true}
      sortable={true}
      data={filterBy(data, filter)}
      filter={filter}
      onFilterChange={(e) => setFilter(e.filter)}
    >
      <GridToolbar>
        Locale:&nbsp;&nbsp;&nbsp;
        <DropDownList
          value={currentLocale}
          textField="language"
          onChange={(e) => {
            handleChange(e);
            // setCurrentLocale(e.target.value);
          }}
          data={locales}
        />
        &nbsp;&nbsp;&nbsp;
        <button
          title="Add new"
          className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary"
          onClick={handalGotoPage}
        >
          New permit
        </button>
        <button className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary">
          Print
        </button>
      </GridToolbar>
      <Column
        field="ProductID"
        title={t("number")}
        width="150px"
        editor="boolean"
      />

      <Column field="ProductName" title={t("title")} width="200px" />
      <Column
        field="FirstOrderedOn"
        title={t("valid_from")}
        editor="date"
        format="{0:d/M/y}"
        filter="date"
        sortable={true}
        filterable={true}
        width="200px"
      />
      <Column
        field="LastOrderedOn"
        title={t("valid_to")}
        editor="date"
        format="{0:d/M/y}"
        filter="date"
        sortable={true}
        filterable={true}
        width="200px"
      />
      <Column field="Status" title={t("work_flow_status")} width="200px" />
      <Column cell={CommandCell} filterable={false} sortable={false} />
    </Grid>
  );
};

export default New_workPermit;
