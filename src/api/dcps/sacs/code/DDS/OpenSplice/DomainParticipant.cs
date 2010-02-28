// The OpenSplice DDS Community Edition project.
//
// Copyright (C) 2006 to 2009 PrismTech Limited and its licensees.
// Copyright (C) 2009  L-3 Communications / IS
// 
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License Version 3 dated 29 June 2007, as published by the
//  Free Software Foundation.
// 
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with OpenSplice DDS Community Edition; if not, write to the Free Software
//  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

using System;
using System.Runtime.InteropServices;
using DDS;
using DDS.OpenSplice.CustomMarshalers;

namespace DDS.OpenSplice
{
    public class DomainParticipant : Entity, IDomainParticipant
    {
        private readonly DomainParticipantListenerHelper listenerHelper;

        /**
         * Constructor is only called by DDS.DomainParticipantFactory.
         */
        internal DomainParticipant(IntPtr gapiPtr)
            : base(gapiPtr)
        {
            listenerHelper = new DomainParticipantListenerHelper();
            builtinTopicRegisterTypeSupport(this);
            builtinTopicCreateWrappers(this);
        }

        /**
         * Constructor is only called by DDS.DomainParticipantFactory.
         */
        internal DomainParticipant(IntPtr gapiPtr, DomainParticipantListenerHelper listenerHelper)
            : base(gapiPtr)
        {
            this.listenerHelper = listenerHelper;
            builtinTopicRegisterTypeSupport(this);
            builtinTopicCreateWrappers(this);
        }

        /**
         * Register the four builtin topics:
         *     "DDS::ParticipantBuiltinTopicData"
         *     "DDS::TopicBuiltinTopicData"
         *     "DDS::PublicationBuiltinTopicData"
         *     "DDS::SubscriptionBuiltinTopicData"
         */
        internal static ReturnCode builtinTopicRegisterTypeSupport(DomainParticipant participant)
        {
            ReturnCode result;

            DDS.ParticipantBuiltinTopicDataTypeSupport DDSParticipant = 
                    new DDS.ParticipantBuiltinTopicDataTypeSupport();
            result = DDSParticipant.RegisterType(participant, DDSParticipant.TypeName);
            if (result != ReturnCode.Ok)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicRegisterTypeSupport",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to register builtin topic: DCPSParticipant");
            }

            DDS.TopicBuiltinTopicDataTypeSupport DDSTopic =
                    new DDS.TopicBuiltinTopicDataTypeSupport();
            result = DDSTopic.RegisterType(participant, DDSTopic.TypeName);
            if (result != ReturnCode.Ok)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicRegisterTypeSupport",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to register builtin topic: DCPSTopic");
            }

            DDS.PublicationBuiltinTopicDataTypeSupport DDSPublication = 
                    new DDS.PublicationBuiltinTopicDataTypeSupport();
            result = DDSPublication.RegisterType(participant, DDSPublication.TypeName);
            if (result != ReturnCode.Ok)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicRegisterTypeSupport",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to register builtin topic: DCPSPublication");
            }

            DDS.SubscriptionBuiltinTopicDataTypeSupport DDSSubscription = 
                    new DDS.SubscriptionBuiltinTopicDataTypeSupport();
            result = DDSSubscription.RegisterType(participant, DDSSubscription.TypeName);
            if (result != ReturnCode.Ok)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicRegisterTypeSupport",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to register builtin topic: DCPSSubscription");
            }
            return result;
        }

        /**
         * Wrap the four builtin topics in C# objects:
         *     "DCPSParticipant"
         *     "DCPSTopic"
         *     "DCPSPublication"
         *     "DCPSSubscription"
         */
        internal static void builtinTopicCreateWrappers(DomainParticipant participant)
        {
            // Lookup the "DCPSParticipant" topic.
            IntPtr gapiPtr = Gapi.DomainParticipant.lookup_topicdescription(participant.GapiPeer, "DCPSParticipant");
            if (gapiPtr == IntPtr.Zero)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicCreateWrappers",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to wrap builtin topic: DCPSParticipant");
            }
            else
            {
                // Wrap the gapi topic in a C# object.
                new Topic(gapiPtr);
                
                // And lookup the "DCPSTopic" topic.
                gapiPtr = Gapi.DomainParticipant.lookup_topicdescription(participant.GapiPeer, "DCPSTopic");
            }
            if (gapiPtr == IntPtr.Zero)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicCreateWrappers",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to wrap builtin topic: DCPSTopic");
            }
            else
            {
                // Wrap the gapi topic in a C# object.
                new Topic(gapiPtr);
                
                // And lookup the "DCPSTopic" topic.
                gapiPtr = Gapi.DomainParticipant.lookup_topicdescription(participant.GapiPeer, "DCPSPublication");
            }
            if (gapiPtr == IntPtr.Zero)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicCreateWrappers",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to wrap builtin topic: DCPSPublication");
            }
            else
            {
                // Wrap the gapi topic in a C# object.
                new Topic(gapiPtr);
                
                // And lookup the "DCPSTopic" topic.
                gapiPtr = Gapi.DomainParticipant.lookup_topicdescription(participant.GapiPeer, "DCPSSubscription");
            }
            if (gapiPtr == IntPtr.Zero)
            {
                DDS.OpenSplice.OS.Report(
                        DDS.OpenSplice.ReportType.OS_ERROR,
                        "DDS.OpenSplice.DomainParticipant.builtinTopicCreateWrappers",
                        "DDS/OpenSplice/DomainParticipant.cs",
                        DDS.ErrorCode.Error,
                        "Failed to wrap builtin topic: DCPSSubscription");
            }
            else
            {
                // Wrap the gapi topic in a C# object.
                new Topic(gapiPtr);
            }

        }
        public ReturnCode SetListener(IDomainParticipantListener listener, StatusKind mask)
        {
            ReturnCode result = ReturnCode.Error;

            if (listener != null)
            {
                Gapi.gapi_domainParticipantListener gapiListener;
                listenerHelper.Listener = listener;
                listenerHelper.CreateListener(out gapiListener);
                lock (listener)
                {
                    using (DomainParticipantListenerMarshaler marshaler = new DomainParticipantListenerMarshaler(ref gapiListener))
                    {
                        result = Gapi.DomainParticipant.set_listener(
                                GapiPeer,
                                marshaler.GapiPtr,
                                mask);
                    }
                }
            }
            else
            {
                result = Gapi.DomainParticipant.set_listener(
                        GapiPeer,
                        IntPtr.Zero,
                        mask);
            }
            return result;
        }

        public IPublisher CreatePublisher()
        {
            return CreatePublisher(null, 0);
        }

        public IPublisher CreatePublisher(IPublisherListener listener, StatusKind mask)
        {
            IPublisher publisher = null;

            if (listener != null)
            {
                // Note: we use the same gapi lister as the DataWriter since the
                // publisher doesn't add anything unique
                OpenSplice.Gapi.gapi_publisherDataWriterListener gapiListener;
                PublisherDataWriterListenerHelper listenerHelper = new PublisherDataWriterListenerHelper();
                listenerHelper.Listener = listener;
                listenerHelper.CreateListener(out gapiListener);
                lock (listener)
                {
                    using (PublisherDataWriterListenerMarshaler listenerMarshaler = new PublisherDataWriterListenerMarshaler(ref gapiListener))
                    {
                        IntPtr gapiPtr = Gapi.DomainParticipant.create_publisher(
                                GapiPeer,
                                Gapi.NativeConstants.GapiPublisherQosDefault,
                                listenerMarshaler.GapiPtr,
                                mask);
                        if (gapiPtr != IntPtr.Zero)
                        {
                            publisher = new Publisher(gapiPtr, listenerHelper);
                        }
                    }
                }
            }
            else
            {
                IntPtr gapiPtr = Gapi.DomainParticipant.create_publisher(
                            GapiPeer,
                            Gapi.NativeConstants.GapiPublisherQosDefault,
                            IntPtr.Zero,
                            mask);
                if (gapiPtr != IntPtr.Zero)
                {
                    publisher = new Publisher(gapiPtr);
                }
            }
            return publisher;
        }

        public IPublisher CreatePublisher(PublisherQos qos)
        {
            return CreatePublisher(qos, null, 0);
        }

        public IPublisher CreatePublisher(PublisherQos qos, IPublisherListener listener, StatusKind mask)
        {
            IPublisher publisher = null;

            using (PublisherQosMarshaler marshaler = new PublisherQosMarshaler())
            {
                // Note: we use the same gapi lister as the DataWriter since the
                // publisher doesn't add anything unique
                if (marshaler.CopyIn(qos) == ReturnCode.Ok)
                {
                    if (listener != null)
                    {
                        Gapi.gapi_publisherDataWriterListener gapiListener;
                        PublisherDataWriterListenerHelper listenerHelper = new PublisherDataWriterListenerHelper();
                        listenerHelper.Listener = listener;
                        listenerHelper.CreateListener(out gapiListener);
                        lock (listener)
                        {
                            using (PublisherDataWriterListenerMarshaler listenerMarshaler =
                                    new PublisherDataWriterListenerMarshaler(ref gapiListener))
                            {
                                IntPtr gapiPtr = Gapi.DomainParticipant.create_publisher(
                                        GapiPeer,
                                        marshaler.GapiPtr,
                                        listenerMarshaler.GapiPtr,
                                        mask);
                                if (gapiPtr != IntPtr.Zero)
                                {
                                    publisher = new Publisher(gapiPtr, listenerHelper);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Invoke the corresponding gapi function.
                        IntPtr gapiPtr = Gapi.DomainParticipant.create_publisher(
                                GapiPeer,
                                marshaler.GapiPtr,
                                IntPtr.Zero,
                                mask);
                        if (gapiPtr != IntPtr.Zero)
                        {
                            publisher = new Publisher(gapiPtr);
                        }
                    }
                }
            }

            return publisher;
        }

        public ReturnCode DeletePublisher(IPublisher p)
        {
            ReturnCode result = ReturnCode.BadParameter;

            Publisher publisher = (Publisher)p;
            if (publisher != null)
            {
                result = Gapi.DomainParticipant.delete_publisher(
                        GapiPeer,
                        publisher.GapiPeer);
            }

            return result;
        }

        public ISubscriber CreateSubscriber()
        {
            //TODO: JLS: This had been sending a StatusKind.Any before
            return CreateSubscriber(null, 0);
        }

        public ISubscriber CreateSubscriber(ISubscriberListener listener, StatusKind mask)
        {
            ISubscriber subscriber = null;

            if (listener != null)
            {
                OpenSplice.Gapi.gapi_subscriberListener gapiListener;
                SubscriberListenerHelper listenerHelper = new SubscriberListenerHelper();
                listenerHelper.Listener = listener;
                listenerHelper.CreateListener(out gapiListener);
                lock (listener)
                {
                    using (SubscriberListenerMarshaler listenerMarshaler =
                            new SubscriberListenerMarshaler(ref gapiListener))
                    {
                        IntPtr gapiPtr = Gapi.DomainParticipant.create_subscriber(
                                GapiPeer,
                                Gapi.NativeConstants.GapiSubscriberQosDefault,
                                listenerMarshaler.GapiPtr,
                                mask);
                        if (gapiPtr != IntPtr.Zero)
                        {
                            subscriber = new Subscriber(gapiPtr, listenerHelper);
                        }
                    }
                }
            }
            else
            {
                IntPtr gapiPtr = Gapi.DomainParticipant.create_subscriber(
                            GapiPeer,
                            Gapi.NativeConstants.GapiSubscriberQosDefault,
                            IntPtr.Zero,
                            mask);
                if (gapiPtr != IntPtr.Zero)
                {
                    subscriber = new Subscriber(gapiPtr);
                }
            }
            return subscriber;
        }

        public ISubscriber CreateSubscriber(SubscriberQos qos)
        {
            return CreateSubscriber(qos, null, 0);
        }

        public ISubscriber CreateSubscriber(SubscriberQos qos, ISubscriberListener listener, StatusKind mask)
        {
            ISubscriber subscriber = null;

            using (SubscriberQosMarshaler marshaler = new SubscriberQosMarshaler())
            {
                if (marshaler.CopyIn(qos) == ReturnCode.Ok)
                {
                    if (listener != null)
                    {
                        OpenSplice.Gapi.gapi_subscriberListener gapiListener;
                        SubscriberListenerHelper listenerHelper = new SubscriberListenerHelper();
                        listenerHelper.Listener = listener;
                        listenerHelper.CreateListener(out gapiListener);
                        lock (listener)
                        {
                            using (SubscriberListenerMarshaler listenerMarshaler =
                                    new SubscriberListenerMarshaler(ref gapiListener))
                            {
                                IntPtr gapiPtr = Gapi.DomainParticipant.create_subscriber(
                                        GapiPeer,
                                        marshaler.GapiPtr,
                                        listenerMarshaler.GapiPtr,
                                        mask);
                                if (gapiPtr != IntPtr.Zero)
                                {
                                    subscriber = new Subscriber(gapiPtr, listenerHelper);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Invoke the corresponding gapi function.
                        IntPtr gapiPtr = Gapi.DomainParticipant.create_subscriber(
                                GapiPeer,
                                marshaler.GapiPtr,
                                IntPtr.Zero,
                                mask);
                        if (gapiPtr != IntPtr.Zero)
                        {
                            subscriber = new Subscriber(gapiPtr);
                        }
                    }
                }
            }

            return subscriber;
        }

        public ReturnCode DeleteSubscriber(ISubscriber s)
        {
            ReturnCode result = ReturnCode.BadParameter;

            Subscriber subscriber = (Subscriber)s;
            if (subscriber != null)
            {
                result = Gapi.DomainParticipant.delete_subscriber(
                        GapiPeer,
                        subscriber.GapiPeer);
            }

            return result;
        }

        public ISubscriber BuiltInSubscriber
        {
            get
            {
                IntPtr subscriber_gapiPtr = Gapi.DomainParticipant.get_builtin_subscriber(GapiPeer);
                ISubscriber subscriber = SacsSuperClass.fromUserData(subscriber_gapiPtr) as ISubscriber;

                // If needed, associate the C# wrapper with the gapiPtr.
                if (subscriber_gapiPtr != IntPtr.Zero && subscriber == null)
                {
                    subscriber = new Subscriber(subscriber_gapiPtr);

                    IntPtr reader_gapiPtr;
                    reader_gapiPtr = Gapi.Subscriber.lookup_datareader(subscriber_gapiPtr, "DCPSTopic");
                    if (reader_gapiPtr != IntPtr.Zero)
                    {
                        DDS.TopicBuiltinTopicDataTypeSupport typeSupport = 
                                GetTypeSupport("DDS::TopicBuiltinTopicData") 
                                        as DDS.TopicBuiltinTopicDataTypeSupport;
                        typeSupport.CreateDataReader(reader_gapiPtr);
                        IntPtr topic_gapiPtr = Gapi.DataReader.get_topicdescription(reader_gapiPtr);
                        if (topic_gapiPtr != IntPtr.Zero)
                        {
                            Topic topic = new OpenSplice.Topic(topic_gapiPtr);
                        }

                    }
                    reader_gapiPtr = Gapi.Subscriber.lookup_datareader(subscriber_gapiPtr, "DCPSParticipant");
                    if (reader_gapiPtr != IntPtr.Zero){
                        DDS.ParticipantBuiltinTopicDataTypeSupport typeSupport = 
                                GetTypeSupport("DDS::ParticipantBuiltinTopicData") 
                                        as DDS.ParticipantBuiltinTopicDataTypeSupport;
                        typeSupport.CreateDataReader(reader_gapiPtr);
                        IntPtr topic_gapiPtr = Gapi.DataReader.get_topicdescription(reader_gapiPtr);
                        if (topic_gapiPtr != IntPtr.Zero)
                        {
                            Topic topic = new OpenSplice.Topic(topic_gapiPtr);
                        }

                    }
                    reader_gapiPtr = Gapi.Subscriber.lookup_datareader(subscriber_gapiPtr, "DCPSPublication");
                    if (reader_gapiPtr != IntPtr.Zero){
                        DDS.PublicationBuiltinTopicDataTypeSupport typeSupport = 
                                GetTypeSupport("DDS::PublicationBuiltinTopicData") 
                                        as DDS.PublicationBuiltinTopicDataTypeSupport;
                        typeSupport.CreateDataReader(reader_gapiPtr);
                        IntPtr topic_gapiPtr = Gapi.DataReader.get_topicdescription(reader_gapiPtr);
                        if (topic_gapiPtr != IntPtr.Zero)
                        {
                            Topic topic = new OpenSplice.Topic(topic_gapiPtr);
                        }

                    }
                    reader_gapiPtr = Gapi.Subscriber.lookup_datareader(subscriber_gapiPtr, "DCPSSubscription");
                    if (reader_gapiPtr != IntPtr.Zero){
                        DDS.SubscriptionBuiltinTopicDataTypeSupport typeSupport = 
                                GetTypeSupport("DDS::SubscriptionBuiltinTopicData") 
                                        as DDS.SubscriptionBuiltinTopicDataTypeSupport;
                        typeSupport.CreateDataReader(reader_gapiPtr);
                        IntPtr topic_gapiPtr = Gapi.DataReader.get_topicdescription(reader_gapiPtr);
                        if (topic_gapiPtr != IntPtr.Zero)
                        {
                            Topic topic = new OpenSplice.Topic(topic_gapiPtr);
                        }

                    }
                }

                return subscriber;
            }
        }

        public ITopic CreateTopic(string topicName, string typeName)
        {
            return CreateTopic(topicName, typeName, null, 0);
        }

        public ITopic CreateTopic(string topicName, string typeName, ITopicListener listener, StatusKind mask)
        {
            ITopic topic = null;

            if (listener != null)
            {
                OpenSplice.Gapi.gapi_topicListener gapiListener;
                TopicListenerHelper listenerHelper = new TopicListenerHelper();
                listenerHelper.Listener = listener;
                listenerHelper.CreateListener(out gapiListener);
                lock (listener)
                {
                    using (TopicListenerMarshaler listenerMarshaler = new TopicListenerMarshaler(ref gapiListener))
                    {
                        IntPtr gapiPtr = Gapi.DomainParticipant.create_topic(
                                GapiPeer,
                                topicName,
                                typeName,
                                Gapi.NativeConstants.GapiTopicQosDefault,
                                listenerMarshaler.GapiPtr,
                                mask);
                        if (gapiPtr != IntPtr.Zero)
                        {
                            topic = new Topic(gapiPtr, listenerHelper);
                        }
                    }
                }
            }
            else
            {
                IntPtr gapiPtr = Gapi.DomainParticipant.create_topic(
                        GapiPeer,
                        topicName,
                        typeName,
                        Gapi.NativeConstants.GapiTopicQosDefault,
                        IntPtr.Zero,
                        mask);

                if (gapiPtr != IntPtr.Zero)
                {
                    topic = new Topic(gapiPtr);
                }
            }

            return topic;
        }

        public ITopic CreateTopic(string topicName, string typeName, TopicQos qos)
        {
            return CreateTopic(topicName, typeName, qos, null, 0);
        }

        public ITopic CreateTopic(string topicName, string typeName, TopicQos qos, ITopicListener listener, StatusKind mask)
        {
            ITopic topic = null;

            using (TopicQosMarshaler marshaler = new TopicQosMarshaler())
            {
                if (marshaler.CopyIn(qos) == ReturnCode.Ok)
                {
                    if (listener != null)
                    {
                        OpenSplice.Gapi.gapi_topicListener gapiListener;
                        TopicListenerHelper listenerHelper = new TopicListenerHelper();
                        listenerHelper.Listener = listener;
                        listenerHelper.CreateListener(out gapiListener);
                        lock (listener)
                        {
                            using (TopicListenerMarshaler listenerMarshaler =
                                    new TopicListenerMarshaler(ref gapiListener))
                            {
                                IntPtr gapiPtr = Gapi.DomainParticipant.create_topic(
                                        GapiPeer,
                                        topicName,
                                        typeName,
                                        marshaler.GapiPtr,
                                        listenerMarshaler.GapiPtr,
                                        mask);
                                if (gapiPtr != IntPtr.Zero)
                                {
                                    topic = new Topic(gapiPtr, listenerHelper);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Invoke the corresponding gapi function.
                        IntPtr gapiPtr = Gapi.DomainParticipant.create_topic(
                                GapiPeer,
                                topicName,
                                typeName,
                                marshaler.GapiPtr,
                                IntPtr.Zero,
                                mask);

                        if (gapiPtr != IntPtr.Zero)
                        {
                            topic = new Topic(gapiPtr);
                        }
                    }
                }
            }

            return topic;
        }

        public ReturnCode DeleteTopic(ITopic t)
        {
            ReturnCode result = ReturnCode.BadParameter;

            Topic topic = t as Topic;
            if (topic != null)
            {
                result = Gapi.DomainParticipant.delete_topic(
                        GapiPeer,
                        topic.GapiPeer);
            }

            return result;
        }

        public ITopic FindTopic(string topicName, Duration timeout)
        {
            IntPtr gapiPtr = Gapi.DomainParticipant.find_topic(
                    GapiPeer,
                    topicName,
                    ref timeout);

            ITopic topic = null;

            if (gapiPtr != IntPtr.Zero)
            {
                topic = new OpenSplice.Topic(gapiPtr);
            }

            return topic;
        }

        public ITopicDescription LookupTopicDescription(string name)
        {
            ITopicDescription topicDesc = null;

            IntPtr gapiPtr = Gapi.DomainParticipant.lookup_topicdescription(
                    GapiPeer,
                    name);

            if (gapiPtr != IntPtr.Zero)
            {
                // if the lookup fails, then we don't have a managed object for gapi object yet
                topicDesc = SacsSuperClass.fromUserData(gapiPtr) as ITopicDescription;

                if (topicDesc == null)
                {
                    DDS.OpenSplice.OS.Report(
                            DDS.OpenSplice.ReportType.OS_ERROR,
                            "DDS.OpenSplice.DomainParticipant.LookupTopicDescription",
                            "DDS/OpenSplice/DomainParticipant.cs",
                            DDS.ErrorCode.EntityUnknown,
                            "TopicDescription Entity has no C# wrapper.");
                }
            }

            return topicDesc;
        }

        public IContentFilteredTopic CreateContentFilteredTopic(
                string name, 
                ITopic relatedTopic,
                string filterExpression, 
                params string[] expressionParameters)
        {
            IContentFilteredTopic contentFilteredTopic = null;

            Topic related = relatedTopic as Topic;
            if (relatedTopic != null)
            {
                using (SequenceStringMarshaler marshaler = new SequenceStringMarshaler())
                {
                    if (marshaler.CopyIn(expressionParameters) == DDS.ReturnCode.Ok)
                    {
                        IntPtr gapiPtr = Gapi.DomainParticipant.create_contentfilteredtopic(
                                GapiPeer,
                                name,
                                related.GapiPeer,
                                filterExpression,
                                marshaler.GapiPtr);
                        if (gapiPtr != IntPtr.Zero)
                        {
                            contentFilteredTopic = new ContentFilteredTopic(gapiPtr);
                        }
                    }

                }
            }

            return contentFilteredTopic;
        }

        public ReturnCode DeleteContentFilteredTopic(IContentFilteredTopic t)
        {
            ReturnCode result = ReturnCode.BadParameter;

            ContentFilteredTopic contentFilteredTopic = t as ContentFilteredTopic;
            if (contentFilteredTopic != null)
            {
                result = Gapi.DomainParticipant.delete_contentfilteredtopic(
                    GapiPeer,
                    contentFilteredTopic.GapiPeer);
            }

            return result;
        }


        public IMultiTopic CreateMultiTopic(
                string name, 
                string typeName,
                string subscriptionExpression, 
                params string[] expressionParameters)
        {
            IMultiTopic multiTopic = null;

            using (SequenceStringMarshaler marshaler = new SequenceStringMarshaler())
            {
                if (marshaler.CopyIn(expressionParameters) == DDS.ReturnCode.Ok)
                {
                    IntPtr gapiPtr = Gapi.DomainParticipant.create_multitopic(
                            GapiPeer,
                            name,
                            typeName,
                            subscriptionExpression,
                            marshaler.GapiPtr);

                    if (gapiPtr != IntPtr.Zero)
                    {
                        multiTopic = new MultiTopic(gapiPtr);
                    }
                }
            }

            return multiTopic;
        }

        public ReturnCode DeleteMultiTopic(IMultiTopic t)
        {
            ReturnCode result = ReturnCode.BadParameter;

            MultiTopic multiTopic = t as MultiTopic;
            if (multiTopic != null)
            {
                result = Gapi.DomainParticipant.delete_multitopic(
                        GapiPeer,
                        multiTopic.GapiPeer);
            }

            return result;
        }

        public ReturnCode DeleteContainedEntities()
        {
            return Gapi.DomainParticipant.delete_contained_entities(
                    GapiPeer,
                    null,
                    IntPtr.Zero);
        }

        public ReturnCode SetQos(DomainParticipantQos qos)
        {
            ReturnCode result;

            using (DomainParticipantQosMarshaler marshaler = new DomainParticipantQosMarshaler())
            {
                result = marshaler.CopyIn(qos);
                if (result == ReturnCode.Ok)
                {
                    result = Gapi.DomainParticipant.set_qos(
                            GapiPeer,
                            marshaler.GapiPtr);
                }
            }
            return result;
        }

        public ReturnCode GetQos(ref DomainParticipantQos qos)
        {
            ReturnCode result;

            using (DomainParticipantQosMarshaler marshaler = new DomainParticipantQosMarshaler())
            {
                result = Gapi.DomainParticipant.get_qos(
                        GapiPeer,
                        marshaler.GapiPtr);

                if (result == ReturnCode.Ok)
                {
                    marshaler.CopyOut(ref qos);
                }
            }

            return result;
        }

        public ReturnCode IgnoreParticipant(InstanceHandle handle)
        {
            return Gapi.DomainParticipant.ignore_participant(
                    GapiPeer,
                    handle);
        }

        public ReturnCode IgnoreTopic(InstanceHandle handle)
        {
            return Gapi.DomainParticipant.ignore_topic(
                    GapiPeer,
                    handle);
        }

        public ReturnCode IgnorePublication(InstanceHandle handle)
        {
            return Gapi.DomainParticipant.ignore_publication(
                    GapiPeer,
                    handle);
        }

        public ReturnCode IgnoreSubscription(InstanceHandle handle)
        {
            return Gapi.DomainParticipant.ignore_subscription(
                    GapiPeer,
                    handle);
        }

        public string DomainId
        {
            get
            {
                IntPtr ptr = Gapi.DomainParticipant.get_domain_id(GapiPeer);
                string result = Marshal.PtrToStringAnsi(ptr);
                Gapi.GenericAllocRelease.Free(ptr);

                return result;
            }
        }

        public ReturnCode AssertLiveliness()
        {
            return Gapi.DomainParticipant.assert_liveliness(GapiPeer);
        }

        public ReturnCode SetDefaultPublisherQos(PublisherQos qos)
        {
            ReturnCode result;

            using (PublisherQosMarshaler marshaler = new PublisherQosMarshaler())
            {
                result = marshaler.CopyIn(qos);
                if (result == ReturnCode.Ok)
                {
                    result = Gapi.DomainParticipant.set_default_publisher_qos(
                            GapiPeer,
                            marshaler.GapiPtr);
                }
            }
            return result;
        }

        public ReturnCode GetDefaultPublisherQos(ref PublisherQos qos)
        {
            ReturnCode result;

            using (PublisherQosMarshaler marshaler = new PublisherQosMarshaler())
            {
                result = Gapi.DomainParticipant.get_default_publisher_qos(
                        GapiPeer,
                        marshaler.GapiPtr);

                if (result == ReturnCode.Ok)
                {
                    marshaler.CopyOut(ref qos);
                }
            }

            return result;
        }

        public ReturnCode SetDefaultSubscriberQos(SubscriberQos qos)
        {
            ReturnCode result;

            using (SubscriberQosMarshaler marshaler = new SubscriberQosMarshaler())
            {
                result = marshaler.CopyIn(qos);
                if (result == ReturnCode.Ok)
                {
                    result = Gapi.DomainParticipant.set_default_subscriber_qos(
                            GapiPeer,
                            marshaler.GapiPtr);
                }
            }
            return result;
        }

        public ReturnCode GetDefaultSubscriberQos(ref SubscriberQos qos)
        {
            ReturnCode result;

            using (SubscriberQosMarshaler marshaler = new SubscriberQosMarshaler())
            {
                result = Gapi.DomainParticipant.get_default_subscriber_qos(
                        GapiPeer,
                        marshaler.GapiPtr);

                if (result == ReturnCode.Ok)
                {
                    marshaler.CopyOut(ref qos);
                }
            }

            return result;
        }

        public ReturnCode SetDefaultTopicQos(TopicQos qos)
        {
            ReturnCode result;

            using (TopicQosMarshaler marshaler = new TopicQosMarshaler())
            {
                result = marshaler.CopyIn(qos);
                if (result == ReturnCode.Ok)
                {
                    result = Gapi.DomainParticipant.set_default_topic_qos(
                            GapiPeer,
                            marshaler.GapiPtr);
                }
            }

            return result;
        }

        public ReturnCode GetDefaultTopicQos(ref TopicQos qos)
        {
            ReturnCode result;

            using (TopicQosMarshaler marshaler = new TopicQosMarshaler())
            {
                result = Gapi.DomainParticipant.get_default_topic_qos(
                        GapiPeer,
                        marshaler.GapiPtr);

                if (result == ReturnCode.Ok)
                {
                    marshaler.CopyOut(ref qos);
                }
            }

            return result;
        }

        public bool ContainsEntity(InstanceHandle handle)
        {
            byte result = Gapi.DomainParticipant.contains_entity(
                    GapiPeer,
                    handle);
            return result != 0;
        }

        public ReturnCode GetCurrentTime(out Time currentTime)
        {
            return Gapi.DomainParticipant.get_current_time(
                    GapiPeer,
                    out currentTime);
        }

        public ITypeSupport GetTypeSupport(string registeredName)
        {
            ITypeSupport typeSupport = null;

            IntPtr gapiPtr = Gapi.DomainParticipant.get_typesupport(
                    GapiPeer,
                    registeredName);

            if (gapiPtr != IntPtr.Zero)
            {
                typeSupport = SacsSuperClass.fromUserData(gapiPtr) as ITypeSupport;
            }

            return typeSupport;
        }


        public ITypeSupport LookupTypeSupport(string registeredTypeName)
        {
            ITypeSupport typeSupport = null;

            IntPtr gapiPtr = Gapi.DomainParticipant.lookup_typesupport(
                GapiPeer,
                registeredTypeName);

            if (gapiPtr != IntPtr.Zero)
            {
                typeSupport = SacsSuperClass.fromUserData(gapiPtr) as ITypeSupport;
            }

            return typeSupport;
        }

        // only for API implementors
        internal IntPtr GetTypeMetaDescription(string typeName)
        {
            return Gapi.DomainParticipant.get_type_metadescription(
                    GapiPeer,
                    typeName);
        }
    }
}
