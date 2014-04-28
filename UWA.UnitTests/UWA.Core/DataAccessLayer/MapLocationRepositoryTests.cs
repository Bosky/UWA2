using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using UWA.Core.BusinessLayer.Contracts;
using UWA.Core.DataAccessLayer;

namespace UWA.UnitTests.UWA.Core.DataAccessLayer
{
    public class when_working_with_the_map_location_repository : Specification
    {
        protected ORMRepository _locationRepository;
        protected IList<MapLocation> _expected;

        protected override void Establish_context()
        {
            base.Establish_context();

            _locationRepository = new ORMRepository();

            _expected = new List<MapLocation>
            {
                new MapLocation {Longitude = 1, Latitude = 2},
                new MapLocation {Longitude = 32, Latitude = 36}
            };
        }
    }

    public class and_asking_for_full_list_of_locations : when_working_with_the_map_location_repository
    {
        private IList<MapLocation> _result;

        protected override void Establish_context()
        {
            base.Establish_context();
        }

        protected override void Because_of()
        {
            _result = _locationRepository.GetLocations();
        }

        [Test]
        public void then_a_valid_list_of_locations_should_be_returned()
        {
            Assert.AreEqual(_expected, _result);
        }
    }


}
