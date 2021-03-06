﻿/*
 * ISO standards can be purchased through the ANSI webstore at https://webstore.ansi.org
*/

using System;
using System.Collections.Generic;
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Representations;
using AgGateway.ADAPT.ISOv4Plugin.ExtensionMethods;
using AgGateway.ADAPT.ISOv4Plugin.Representation;

namespace AgGateway.ADAPT.ISOv4Plugin.Mappers
{
    public interface IEnumeratedValueMapper
    {
        UInt32 Map(ISOEnumeratedMeter currentMeter, List<WorkingData> meters, SpatialRecord spatialRecord);
    }

    public class EnumeratedValueMapper : IEnumeratedValueMapper
    {
        private readonly IEnumeratedMeterFactory _enumeratedMeterFactory;
        private readonly IRepresentationMapper _representationMapper;

        public EnumeratedValueMapper() : this (new EnumeratedMeterFactory(), new RepresentationMapper())
        {
            
        }

        public EnumeratedValueMapper(IEnumeratedMeterFactory enumeratedMeterFactory, IRepresentationMapper representationMapper)
        {
            _enumeratedMeterFactory = enumeratedMeterFactory;
            _representationMapper = representationMapper;
        }

        public UInt32 Map(ISOEnumeratedMeter currentMeter, List<WorkingData> meters, SpatialRecord spatialRecord)
        {
            var matchingMeters = meters.Where(x => x.Id.FindIntIsoId() == currentMeter.Id.FindIntIsoId()).ToList();
            var ddi = _representationMapper.Map(currentMeter.Representation);

            if (ddi == 141 && currentMeter.DeviceElementUseId != 0)
                ddi = 161;

            var creator = _enumeratedMeterFactory.GetMeterCreator(ddi.GetValueOrDefault());
            return creator.GetMetersValue(matchingMeters, spatialRecord);
        }
    }
}
