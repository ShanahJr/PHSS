using PHSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHSS.ViewModels
{
    public class WebsiteIndexViewModel
    {
        public WebsiteIndexViewModel(List<FixtureModel> Fixtures, List<ResultModel> Results, List<LogModel> Logs)
        {
            mFixtures = Fixtures;
            mResults = Results;
            mLogs = Logs;
        }

        //Data Memmbers
        private List<FixtureModel> mFixtures;
        private List<ResultModel> mResults;
        private List<LogModel> mLogs;

        //Properties
        public List<FixtureModel> Fixtures
        {
            get
            {
                return mFixtures;
            }

            set
            {
                mFixtures = value;
            }
        }

        public List<ResultModel> Results
        {
            get
            {
                return mResults;
            }

            set
            {
                mResults = value;
            }
        }

        public List<LogModel> Logs
        {
            get
            {
                return mLogs;
            }

            set
            {
                mLogs = value;
            }
        }
    }
}