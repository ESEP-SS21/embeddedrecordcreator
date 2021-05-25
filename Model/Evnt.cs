namespace EmbeddedRecordCreator.Model
{
    public class Evnt
    {
        public EventType Type { get; set; }
        public int Payl { get; set; }
        public bool Broad { get; set; }
    }

    public enum EventType
    {
        EVNT_ERR,
        EVNT_ERR_GONE,
        EVNT_WRN,
        EVNT_WRN_GONE,
        EVNT_HRTB,
        EVNT_ACK,
        EVNT_SEN_LB_ST_BLCK,
        EVNT_SEN_LB_ST_CLR,
        EVNT_SEN_LB_SW_BLCK,
        EVNT_SEN_LB_SW_CLR,
        EVNT_SEN_LB_RA_BLCK,
        EVNT_SEN_LB_RA_CLR,
        EVNT_SEN_LB_EN_BLCK,
        EVNT_SEN_LB_EN_CLR,
        EVNT_SEN_LB_HE_BLCK,
        EVNT_SEN_LB_HE_CLR,
        EVNT_SEN_METAL_DTC,
        EVNT_SEN_HEIGHT_REQ,
        EVNT_SEN_HEIGHT_HE,
        EVNT_SEN_ESTOP_ON,
        EVNT_SEN_ESTOP_OFF,
        EVNT_CTRL_T_STR_PRS_LNG,
        EVNT_CTRL_T_STR_PRS_SRT,
        EVNT_CTRL_T_STP_PRS_LNG,
        EVNT_CTRL_T_STP_PRS_SRT,
        EVNT_CTRL_T_RST_PRS_LNG,
        EVNT_CTRL_T_RST_PRS_SRT,
        EVNT_ACT_CTRL_T_STR_LED_ON,
        EVNT_ACT_CTRL_T_STR_LED_OFF,
        EVNT_ACT_CTRL_T_RST_LED_ON,
        EVNT_ACT_CTRL_T_RST_LED_OFF,
        EVNT_ACT_BELT_BWD,
        EVNT_ACT_BELT_FWD,
        EVNT_ACT_BELT_STP,
        EVNT_ACT_SORT_DSC,
        EVNT_ACT_SORT_NO_DSC,
        EVNT_ACT_SORT_RST,
        EVNT_ACT_STPL_LED_ON,
        EVNT_ACT_STPL_LED_OFF,
        EVNT_ACT_STPL_LED_BLNK_FST,
        EVNT_ACT_STPL_LED_BLNK_SLW,
        EVNT_WRPC_TRNS_RQ,
        EVNT_WRPC_FLP,
        EVNT_MOD_IDL,
        EVNT_MOD_OP,
        EVNT_MOD_ERR,
        EVNT_TIM_REQ,
        EVNT_TIM_ALRT
    }
}