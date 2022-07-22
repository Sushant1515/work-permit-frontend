import React from "react";
import { useTranslation } from "react-i18next";

function MyComponenet() {
  const { t } = useTranslation();
  return <div>{t("localization_testing")}</div>;
}

export default MyComponenet;